namespace Maticsoft.TaoBao.Stream.Message
{
    using System;

    public interface ITopCometMessageListener
    {
        void OnClientKickOff();
        void OnConnectMsg(string message);
        void OnConnectReachMaxTime();
        void OnDiscardMsg(string message);
        void OnException(Exception ex);
        void OnHeartBeat();
        void OnOtherMsg(string message);
        void OnReceiveMsg(string message);
        void OnServerKickOff();
        void OnServerRehash();
        void OnServerUpgrade(string message);
    }
}

