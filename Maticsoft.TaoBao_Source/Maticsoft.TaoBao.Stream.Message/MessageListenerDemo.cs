namespace Maticsoft.TaoBao.Stream.Message
{
    using System;

    public class MessageListenerDemo : ITopCometMessageListener
    {
        public void OnClientKickOff()
        {
        }

        public void OnConnectMsg(string message)
        {
            Console.WriteLine(DateTime.Now.ToString() + ":Connected..." + message);
        }

        public void OnConnectReachMaxTime()
        {
        }

        public void OnDiscardMsg(string message)
        {
        }

        public void OnException(Exception ex)
        {
        }

        public void OnHeartBeat()
        {
            Console.WriteLine(DateTime.Now.ToString() + ":HeartBeat...");
        }

        public void OnOtherMsg(string message)
        {
        }

        public void OnReceiveMsg(string message)
        {
            Console.WriteLine(DateTime.Now.ToString() + ":ReceiveMsg:" + message);
        }

        public void OnServerKickOff()
        {
        }

        public void OnServerRehash()
        {
        }

        public void OnServerUpgrade(string message)
        {
        }
    }
}

