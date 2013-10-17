namespace Maticsoft.TaoBao.Stream.Connect
{
    using System;
    using System.Collections.Generic;

    public interface IHttpConnectionConfiguration
    {
        string GetConnectUrl();
        int GetHttpConnectionTimeout();
        int GetHttpConnectRetryCount();
        int GetHttpConnectRetryInterval();
        int GetHttpReadTimeout();
        int GetHttpReconnectInterval();
        IDictionary<string, string> GetRequestHeader();
        int GetSleepTimeOfServerInUpgrade();
    }
}

