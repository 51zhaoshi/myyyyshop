namespace Maticsoft.TaoBao.Stream.Connect
{
    using System;

    public class ConnectionListenerDemo : IConnectionLifeCycleListener
    {
        public void OnBeforeConnect()
        {
        }

        public void OnConnect()
        {
            Console.WriteLine("Connected...");
        }

        public void OnConnectError(Exception e)
        {
        }

        public void OnException(Exception throwable)
        {
        }

        public void OnMaxReadTimeoutException()
        {
        }

        public void OnReadTimeout()
        {
        }

        public void OnSysErrorException(Exception e)
        {
        }
    }
}

