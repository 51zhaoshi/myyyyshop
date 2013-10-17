namespace Maticsoft.TaoBao.Stream
{
    using Maticsoft.TaoBao.Stream.Connect;
    using Maticsoft.TaoBao.Stream.Message;
    using System;
    using System.Collections.Generic;

    public class Configuration : IHttpConnectionConfiguration, IMessageHandlerConfiguration
    {
        private List<TopCometStreamRequest> connectReqParam;
        private string connectUrl;
        private int httpConnectionTimeout;
        private int httpConnectRetryCount;
        private int httpConnectRetryInterval;
        private int httpReadTimeout;
        private int httpReconnectInterval;
        private int maxThreads;
        private int minThreads;
        private int queueSize;
        private IDictionary<string, string> reqHeader;
        private int sleepTimeOfServerInUpgrade;

        public Configuration(List<TopCometStreamRequest> cometRequest)
        {
            this.connectUrl = "http://stream.api.taobao.com/stream";
            this.httpConnectionTimeout = 5;
            this.httpReadTimeout = 90;
            this.httpConnectRetryCount = 3;
            this.httpConnectRetryInterval = 0x10;
            this.sleepTimeOfServerInUpgrade = 300;
            this.httpReconnectInterval = 0x15054;
            this.minThreads = 100;
            this.maxThreads = 200;
            this.queueSize = 0xc350;
            if ((cometRequest == null) || ((cometRequest != null) && (cometRequest.Count == 0)))
            {
                throw new Exception("comet request param is null");
            }
            this.connectReqParam = cometRequest;
        }

        public Configuration(string appkey, string secret, string userid)
        {
            this.connectUrl = "http://stream.api.taobao.com/stream";
            this.httpConnectionTimeout = 5;
            this.httpReadTimeout = 90;
            this.httpConnectRetryCount = 3;
            this.httpConnectRetryInterval = 0x10;
            this.sleepTimeOfServerInUpgrade = 300;
            this.httpReconnectInterval = 0x15054;
            this.minThreads = 100;
            this.maxThreads = 200;
            this.queueSize = 0xc350;
            TopCometStreamRequest item = new TopCometStreamRequest(appkey, secret, userid, null);
            this.connectReqParam = new List<TopCometStreamRequest>(1);
            this.connectReqParam.Add(item);
        }

        public Configuration(string appkey, string secret, string userid, string connectId)
        {
            this.connectUrl = "http://stream.api.taobao.com/stream";
            this.httpConnectionTimeout = 5;
            this.httpReadTimeout = 90;
            this.httpConnectRetryCount = 3;
            this.httpConnectRetryInterval = 0x10;
            this.sleepTimeOfServerInUpgrade = 300;
            this.httpReconnectInterval = 0x15054;
            this.minThreads = 100;
            this.maxThreads = 200;
            this.queueSize = 0xc350;
            TopCometStreamRequest item = new TopCometStreamRequest(appkey, secret, userid, connectId);
            this.connectReqParam = new List<TopCometStreamRequest>(1);
            this.connectReqParam.Add(item);
        }

        public List<TopCometStreamRequest> GetConnectReqParam()
        {
            return this.connectReqParam;
        }

        public string GetConnectUrl()
        {
            return this.connectUrl;
        }

        public int GetHttpConnectionTimeout()
        {
            return this.httpConnectionTimeout;
        }

        public int GetHttpConnectRetryCount()
        {
            return this.httpConnectRetryCount;
        }

        public int GetHttpConnectRetryInterval()
        {
            return this.httpConnectRetryInterval;
        }

        public int GetHttpReadTimeout()
        {
            return this.httpReadTimeout;
        }

        public int GetHttpReconnectInterval()
        {
            return this.httpReconnectInterval;
        }

        public int GetMaxThreads()
        {
            return this.maxThreads;
        }

        public int GetMinThreads()
        {
            return this.minThreads;
        }

        public int GetQueueSize()
        {
            return this.queueSize;
        }

        public IDictionary<string, string> GetRequestHeader()
        {
            return this.reqHeader;
        }

        public int GetSleepTimeOfServerInUpgrade()
        {
            return this.sleepTimeOfServerInUpgrade;
        }

        public void SetConnectUrl(string connectUrl)
        {
            this.connectUrl = connectUrl;
        }

        public void SetHttpConnectionTimeout(int httpConnectionTimeout)
        {
            this.httpConnectionTimeout = httpConnectionTimeout;
        }

        public void SetHttpConnectRetryCount(int httpConnectRetryCount)
        {
            this.httpConnectRetryCount = httpConnectRetryCount;
        }

        public void SetHttpConnectRetryInterval(int httpConnectRetryInterval)
        {
            this.httpConnectRetryInterval = httpConnectRetryInterval;
        }

        public void SetHttpReadTimeout(int httpReadTimeout)
        {
            this.httpReadTimeout = httpReadTimeout;
        }

        public void SetHttpReconnectInterval(int httpReconnectInterval)
        {
            this.httpReconnectInterval = httpReconnectInterval;
        }

        public void SetMaxThreads(int maxThreads)
        {
            this.maxThreads = maxThreads;
        }

        public void SetMinThreads(int minThreads)
        {
            this.minThreads = minThreads;
        }

        public void SetQueueSize(int queueSize)
        {
            this.queueSize = queueSize;
        }

        public void SetRequestHeader(IDictionary<string, string> reqHeader)
        {
            this.reqHeader = reqHeader;
        }

        public void SetSleepTimeOfServerInUpgrade(int sleepSecond)
        {
            this.sleepTimeOfServerInUpgrade = sleepSecond;
        }
    }
}

