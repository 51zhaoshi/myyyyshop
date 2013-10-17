namespace Maticsoft.TaoBao.Stream.Message
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Stream;
    using Maticsoft.TaoBao.Stream.Connect;
    using System;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class MessageStreamImpl : AbstractStreamImpl
    {
        private TopCometStreamImpl cometStreamImpl;
        private ITopLogger logger;
        private ITopCometMessageListener msgListener;
        private object objLock;
        private string pattern;

        public MessageStreamImpl(StreamMsgConsumeFactory msgConsumeFactory, HttpResponse response, ITopCometMessageListener msgListener, TopCometStreamImpl cometStreamImpl) : base(msgConsumeFactory, response)
        {
            this.logger = new DefaultTopLogger();
            this.pattern = "\\{\"packet\":\\{\"code\":(?<code>(\\d+))(,\"msg\":(?<msg>((.+))))?\\}\\}";
            this.objLock = new object();
            this.msgListener = msgListener;
            this.objLock = cometStreamImpl.GetControlLock();
            this.cometStreamImpl = cometStreamImpl;
        }

        public override void Close()
        {
            base.streamAlive = false;
        }

        public override ITopCometMessageListener GetMessageListener()
        {
            return this.msgListener;
        }

        public override void OnException(Exception ex)
        {
            this.logger.Error(ex.Message);
        }

        public override string ParseLine(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                try
                {
                    MatchCollection matchs = new Regex(this.pattern, RegexOptions.Compiled).Matches(msg);
                    if (matchs.Count > 0)
                    {
                        string str = matchs[0].Groups["code"].Value;
                        if ("202".Equals(str))
                        {
                            return matchs[0].Groups["msg"].Value;
                        }
                        if ("201".Equals(str))
                        {
                            this.msgListener.OnHeartBeat();
                        }
                        else if ("101".Equals(str))
                        {
                            this.msgListener.OnConnectReachMaxTime();
                            this.WakeUp(str);
                        }
                        else if ("203".Equals(str))
                        {
                            this.msgListener.OnDiscardMsg(matchs[0].Groups["msg"].Value.ToString());
                        }
                        else if ("102".Equals(str))
                        {
                            this.msgListener.OnServerUpgrade(matchs[0].Groups["msg"].Value.ToString());
                            this.WakeUp(str);
                        }
                        else if ("103".Equals(str))
                        {
                            this.msgListener.OnServerRehash();
                            this.WakeUp(str);
                        }
                        else if ("104".Equals(str))
                        {
                            this.msgListener.OnClientKickOff();
                            this.WakeUp(str);
                        }
                        else if ("105".Equals(str))
                        {
                            this.msgListener.OnServerKickOff();
                            this.WakeUp(str);
                        }
                        else if ("200".Equals(str))
                        {
                            this.msgListener.OnConnectMsg(matchs[0].Groups["msg"].Value);
                        }
                        else
                        {
                            this.msgListener.OnOtherMsg(matchs[0].Groups["msg"].Value);
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.logger.Error("Message is invalid:" + msg + exception.Message);
                    this.msgListener.OnException(exception);
                    return null;
                }
            }
            return null;
        }

        private void WakeUp(string code)
        {
            try
            {
                Monitor.Enter(this.objLock);
                this.cometStreamImpl.SetServerRespCode(code);
                Monitor.PulseAll(this.objLock);
            }
            catch (Exception)
            {
            }
            finally
            {
                Monitor.Exit(this.objLock);
            }
        }
    }
}

