namespace Maticsoft.TaoBao.Stream
{
    using Maticsoft.TaoBao.Stream.Connect;
    using Maticsoft.TaoBao.Stream.Message;
    using System;

    public interface ITopCometStream
    {
        void SetConnectionListener(IConnectionLifeCycleListener connectionLifeCycleListener);
        void SetMessageListener(ITopCometMessageListener cometMessageListener);
        void Start();
        void Stop();
    }
}

