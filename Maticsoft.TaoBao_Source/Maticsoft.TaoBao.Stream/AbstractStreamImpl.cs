namespace Maticsoft.TaoBao.Stream
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Stream.Connect;
    using Maticsoft.TaoBao.Stream.Message;
    using System;
    using System.Threading;

    public abstract class AbstractStreamImpl : IStreamImplementation
    {
        private ITopLogger log = new DefaultTopLogger();
        private StreamMsgConsumeFactory msgConsumeFactory;
        protected HttpResponse response;
        protected bool streamAlive = true;

        public AbstractStreamImpl(StreamMsgConsumeFactory msgConsumeFactory, HttpResponse response)
        {
            this.msgConsumeFactory = msgConsumeFactory;
            this.response = response;
        }

        public abstract void Close();
        public abstract ITopCometMessageListener GetMessageListener();
        public bool IsAlive()
        {
            return this.streamAlive;
        }

        public void NextMsg()
        {
            if (!this.streamAlive)
            {
                throw new Exception("Stream closed");
            }
            try
            {
                WaitCallback callback = null;
                string line = this.response.GetMsg();
                if (string.IsNullOrEmpty(line))
                {
                    this.streamAlive = false;
                    this.response.Close();
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    if (callback == null)
                    {
                        callback = delegate (object obj) {
                            string str = this.ParseLine(line);
                            if (!string.IsNullOrEmpty(str))
                            {
                                this.GetMessageListener().OnReceiveMsg(str);
                            }
                        };
                    }
                    this.msgConsumeFactory.Consume(callback);
                }
            }
            catch (Exception exception)
            {
                this.response.Close();
                this.streamAlive = false;
                throw exception;
            }
        }

        public abstract void OnException(Exception ex);
        public abstract string ParseLine(string msg);
    }
}

