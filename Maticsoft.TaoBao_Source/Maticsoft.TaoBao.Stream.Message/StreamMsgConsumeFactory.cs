namespace Maticsoft.TaoBao.Stream.Message
{
    using System;
    using System.Threading;

    public class StreamMsgConsumeFactory
    {
        private int maxThreads;
        private int minThreads;
        private int queueSize;

        public StreamMsgConsumeFactory(int minThreads, int maxThreads, int queueSize)
        {
            if (((minThreads <= 0) || (maxThreads <= 0)) || (queueSize <= 0))
            {
                throw new Exception("minThread,maxThread and queueSize must large than 0");
            }
            this.minThreads = minThreads;
            this.maxThreads = maxThreads;
            this.queueSize = queueSize;
            ThreadPool.SetMinThreads(this.minThreads, this.minThreads);
            ThreadPool.SetMaxThreads(this.maxThreads, this.maxThreads);
        }

        public void Consume(WaitCallback callback)
        {
            ThreadPool.QueueUserWorkItem(callback);
        }

        public void Shutdown()
        {
        }
    }
}

