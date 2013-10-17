namespace Maticsoft.TaoBao.Stream.Message
{
    using System;

    public interface IMessageHandlerConfiguration
    {
        int GetMaxThreads();
        int GetMinThreads();
        int GetQueueSize();
    }
}

