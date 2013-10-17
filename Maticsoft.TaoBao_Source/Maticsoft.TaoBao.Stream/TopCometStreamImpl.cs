namespace Maticsoft.TaoBao.Stream
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Stream.Connect;
    using Maticsoft.TaoBao.Stream.Message;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class TopCometStreamImpl : ITopCometStream
    {
        public bool allStop;
        public bool bstop;
        private bool closed;
        private ITopCometMessageListener cometMessageListener;
        private Configuration conf;
        private IConnectionLifeCycleListener connectionListener;
        private List<Thread> controlThreads = new List<Thread>();
        public IStreamImplementation currentStreamImpl;
        private bool isReconnect;
        private long lastStartConsumeThread = DateTime.Now.Ticks;
        private ITopLogger logger = new DefaultTopLogger();
        private StreamMsgConsumeFactory msgConsumeFactory;
        private object objLock = new object();
        public string serverRespCode = "501";
        private int startConsumeThreadTimes;

        public TopCometStreamImpl(Configuration conf)
        {
            this.conf = conf;
        }

        private void ControlThread(StreamMsgConsumeFactory msgConsumeFactory, ref bool bstop, Configuration conf, TopCometStreamRequest cometReq)
        {
            long ticks = 0L;
            while (!bstop)
            {
                if (this.allStop)
                {
                    break;
                }
                try
                {
                    if ("102".Equals(this.serverRespCode))
                    {
                        this.logger.Info("Server is upgrade sleep " + conf.GetSleepTimeOfServerInUpgrade() + " seconds");
                        Thread.Sleep((int) (conf.GetSleepTimeOfServerInUpgrade() * 0x3e8));
                        this.StartConsumeThread(cometReq);
                    }
                    else if (("501".Equals(this.serverRespCode) || "103".Equals(this.serverRespCode)) || ("101".Equals(this.serverRespCode) || "500".Equals(this.serverRespCode)))
                    {
                        this.StartConsumeThread(cometReq);
                    }
                    else
                    {
                        if ("104".Equals(this.serverRespCode) || "105".Equals(this.serverRespCode))
                        {
                            if ((!"104".Equals(this.serverRespCode) || this.isReconnect) && !"105".Equals(this.serverRespCode))
                            {
                                goto Label_0117;
                            }
                        }
                        else
                        {
                            bstop = true;
                        }
                        break;
                    }
                Label_0117:
                    try
                    {
                        Monitor.Enter(this.objLock);
                        ticks = DateTime.Now.Ticks;
                        Monitor.Wait(this.objLock, (int) (conf.GetHttpReconnectInterval() * 0x3e8));
                        if ((DateTime.Now.Ticks - ticks) >= ((conf.GetHttpReconnectInterval() * 0x3e8) * 0x2710))
                        {
                            this.serverRespCode = "500";
                            this.isReconnect = true;
                        }
                    }
                    catch (Exception exception)
                    {
                        this.logger.Error(exception.Message);
                    }
                    finally
                    {
                        Monitor.Exit(this.objLock);
                    }
                    continue;
                }
                catch (Exception exception2)
                {
                    this.logger.Error("Occur some error,stop the stream consume" + exception2.Message);
                    bstop = true;
                    try
                    {
                        Monitor.Enter(this.objLock);
                        Monitor.PulseAll(this.objLock);
                    }
                    finally
                    {
                        Monitor.Exit(this.objLock);
                    }
                    continue;
                }
            }
            if (this.currentStreamImpl != null)
            {
                try
                {
                    this.currentStreamImpl.Close();
                }
                catch (Exception)
                {
                }
            }
            this.logger.Info("Stop stream consume");
        }

        public object GetControlLock()
        {
            return this.objLock;
        }

        public IStreamImplementation GetMsgStreamImpl(TopCometStreamRequest cometReq)
        {
            if (cometReq != null)
            {
                cometReq.GetConnectListener().OnBeforeConnect();
            }
            TopDictionary parameters = new TopDictionary();
            parameters.Add("app_key", cometReq.GetAppkey());
            if (!string.IsNullOrEmpty(cometReq.GetUserId()))
            {
                parameters.Add("user", cometReq.GetUserId());
            }
            if (!string.IsNullOrEmpty(cometReq.GetConnectId()))
            {
                parameters.Add("id", cometReq.GetConnectId());
            }
            parameters.Add("timestamp", DateTime.Now.Ticks);
            IDictionary<string, string> otherParam = cometReq.GetOtherParam();
            if ((otherParam != null) && (otherParam.Count > 0))
            {
                IEnumerator<KeyValuePair<string, string>> enumerator = otherParam.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, string> current = enumerator.Current;
                    KeyValuePair<string, string> pair2 = enumerator.Current;
                    parameters.Add(current.Key, pair2.Value);
                }
            }
            string str = null;
            try
            {
                str = TopUtils.SignTopRequest(parameters, cometReq.GetSecret(), true);
                if (string.IsNullOrEmpty(str))
                {
                    throw new Exception("Get sign error");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            parameters.Add("sign", str);
            HttpResponse response = new HttpClient(this.conf, parameters).Post();
            return (this.currentStreamImpl = new MessageStreamImpl(this.msgConsumeFactory, response, this.cometMessageListener, this));
        }

        public void SetConnectionListener(IConnectionLifeCycleListener connectionLifeCycleListener)
        {
            this.connectionListener = connectionLifeCycleListener;
        }

        public void SetMessageListener(ITopCometMessageListener cometMessageListener)
        {
            this.cometMessageListener = cometMessageListener;
        }

        public void SetServerRespCode(string serverRespCode)
        {
            this.serverRespCode = serverRespCode;
        }

        public void Start()
        {
            if (this.cometMessageListener == null)
            {
                throw new Exception("Comet message listener must not null");
            }
            List<TopCometStreamRequest> connectReqParam = this.conf.GetConnectReqParam();
            this.msgConsumeFactory = new StreamMsgConsumeFactory(this.conf.GetMinThreads(), this.conf.GetMaxThreads(), this.conf.GetQueueSize());
            for (int i = 0; i < connectReqParam.Count; i++)
            {
                try
                {
                    TopCometStreamRequest cometRequest = connectReqParam[i];
                    if (cometRequest.GetConnectListener() == null)
                    {
                        cometRequest.SetConnectListener(this.connectionListener);
                    }
                    if (cometRequest.GetMsgListener() == null)
                    {
                        cometRequest.SetMsgListener(this.cometMessageListener);
                    }
                    Thread item = new Thread(delegate {
                        this.ControlThread(this.msgConsumeFactory, ref this.bstop, this.conf, cometRequest);
                    }) {
                        Name = "stream-control-thread-connectid-" + cometRequest.GetConnectId()
                    };
                    item.Start();
                    this.controlThreads.Add(item);
                }
                catch (Exception)
                {
                }
            }
        }

        private void StartConsumeThread(TopCometStreamRequest cometReq)
        {
            IStreamImplementation stream = null;
            try
            {
                stream = this.GetMsgStreamImpl(cometReq);
                if (cometReq.GetConnectListener() != null)
                {
                    cometReq.GetConnectListener().OnConnect();
                }
            }
            catch (TopCometSysErrorException exception)
            {
                this.bstop = true;
                this.logger.Error(exception.Message);
                if (cometReq.GetConnectListener() != null)
                {
                    cometReq.GetConnectListener().OnSysErrorException(exception);
                }
            }
            catch (Exception exception2)
            {
                this.bstop = true;
                this.logger.Error(exception2.Message);
                if (cometReq.GetConnectListener() != null)
                {
                    cometReq.GetConnectListener().OnConnectError(exception2);
                }
            }
            this.lastStartConsumeThread = DateTime.Now.Ticks;
            new Thread(delegate {
                this.TopCometStreamConsume(this.lastStartConsumeThread, ref this.bstop, stream, cometReq.GetConnectListener());
            }) { Name = "top-stream-consume-thread" + cometReq.GetConnectId() }.Start();
        }

        public void Stop()
        {
            this.allStop = true;
            try
            {
                Monitor.Enter(this.objLock);
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

        private void TopCometStreamConsume(long lastStartConsumeThread, ref bool bstop, IStreamImplementation stream, IConnectionLifeCycleListener connectListener)
        {
            this.startConsumeThreadTimes = 0;
            while ((!this.allStop && !this.closed) && stream.IsAlive())
            {
                try
                {
                    stream.NextMsg();
                    continue;
                }
                catch (Exception)
                {
                    if (stream != null)
                    {
                        try
                        {
                            stream.Close();
                        }
                        catch (Exception exception)
                        {
                            this.logger.Error(exception.Message);
                        }
                    }
                    stream = null;
                    this.closed = true;
                    if (this.connectionListener != null)
                    {
                        try
                        {
                            this.connectionListener.OnReadTimeout();
                        }
                        catch (Exception exception2)
                        {
                            this.logger.Error(exception2.Message);
                        }
                    }
                    if ((DateTime.Now.Ticks - lastStartConsumeThread) < 0x430e23400L)
                    {
                        this.startConsumeThreadTimes++;
                        if (this.startConsumeThreadTimes >= 10)
                        {
                            bstop = true;
                            if (this.connectionListener != null)
                            {
                                try
                                {
                                    this.connectionListener.OnMaxReadTimeoutException();
                                }
                                catch (Exception exception3)
                                {
                                    this.logger.Error(exception3.Message);
                                }
                            }
                            this.logger.Error("Occure too many exception,stop the system,please check");
                            try
                            {
                                try
                                {
                                    Monitor.Enter(this.objLock);
                                    Monitor.PulseAll(this.objLock);
                                }
                                catch (Exception)
                                {
                                }
                                continue;
                            }
                            finally
                            {
                                Monitor.Exit(this.objLock);
                            }
                        }
                        this.startConsumeThreadTimes = 0;
                        this.serverRespCode = "500";
                        try
                        {
                            Monitor.Enter(this.objLock);
                            Monitor.PulseAll(this.objLock);
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            Monitor.Exit(this.objLock);
                        }
                        this.closed = false;
                    }
                    else
                    {
                        Console.WriteLine(" 通知重连" + DateTime.Now.ToString());
                        this.startConsumeThreadTimes = 0;
                        this.serverRespCode = "500";
                        try
                        {
                            Monitor.Enter(this.objLock);
                            Console.WriteLine(" PulseAll" + DateTime.Now.ToString());
                            Monitor.PulseAll(this.objLock);
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            Monitor.Exit(this.objLock);
                        }
                        this.closed = false;
                    }
                    break;
                }
            }
            if (stream != null)
            {
                try
                {
                    stream.Close();
                }
                catch (Exception exception4)
                {
                    this.logger.Warn(exception4.Message);
                }
            }
        }
    }
}

