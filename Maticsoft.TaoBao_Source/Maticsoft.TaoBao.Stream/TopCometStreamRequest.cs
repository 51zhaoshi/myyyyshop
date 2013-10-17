namespace Maticsoft.TaoBao.Stream
{
    using Maticsoft.TaoBao.Stream.Connect;
    using Maticsoft.TaoBao.Stream.Message;
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class TopCometStreamRequest
    {
        private string appkey;
        private string connectId;
        private IConnectionLifeCycleListener connectListener;
        private ITopCometMessageListener msgListener;
        private IDictionary<string, string> otherParam;
        private string secret;
        private string userId;

        public TopCometStreamRequest(string appkey, string secret, string userId, string connectId)
        {
            if (string.IsNullOrEmpty(appkey))
            {
                throw new Exception("appkey is null");
            }
            if (string.IsNullOrEmpty(secret))
            {
                throw new Exception("secret is null");
            }
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    long.Parse(userId);
                    goto Label_0050;
                }
                catch (Exception)
                {
                    throw new Exception("userid must a number type");
                }
            }
            userId = "-1";
        Label_0050:
            if (string.IsNullOrEmpty(connectId))
            {
                this.connectId = GetDefaultConnectId();
            }
            else
            {
                this.connectId = connectId;
            }
            this.appkey = appkey;
            this.secret = secret;
            this.userId = userId;
        }

        public string GetAppkey()
        {
            return this.appkey;
        }

        public string GetConnectId()
        {
            return this.connectId;
        }

        public IConnectionLifeCycleListener GetConnectListener()
        {
            return this.connectListener;
        }

        private static string GetDefaultConnectId()
        {
            string str2;
            try
            {
                str2 = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public ITopCometMessageListener GetMsgListener()
        {
            return this.msgListener;
        }

        public IDictionary<string, string> GetOtherParam()
        {
            return this.otherParam;
        }

        public string GetSecret()
        {
            return this.secret;
        }

        public string GetUserId()
        {
            return this.userId;
        }

        public void SetConnectListener(IConnectionLifeCycleListener connectListener)
        {
            this.connectListener = connectListener;
        }

        public void SetMsgListener(ITopCometMessageListener msgListener)
        {
            this.msgListener = msgListener;
        }

        public void SetOtherParam(IDictionary<string, string> otherParam)
        {
            this.otherParam = otherParam;
        }
    }
}

