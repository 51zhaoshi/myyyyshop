namespace Maticsoft.TaoBao.Stream.Connect
{
    using System;

    public interface IConnectionLifeCycleListener
    {
        void OnBeforeConnect();
        void OnConnect();
        void OnConnectError(Exception e);
        void OnException(Exception throwable);
        void OnMaxReadTimeoutException();
        void OnReadTimeout();
        void OnSysErrorException(Exception e);
    }
}

