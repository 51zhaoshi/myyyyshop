namespace Maticsoft.TaoBao.Jushita
{
    using Maticsoft.TaoBao.Stream.Connect;
    using System;
    using System.Collections.Generic;

    public class JushitaConnectionLifeCycleListener : IConnectionLifeCycleListener
    {
        private JushitaConfiguration conf;

        public JushitaConnectionLifeCycleListener(JushitaConfiguration conf)
        {
            this.conf = conf;
        }

        public void OnBeforeConnect()
        {
            if (this.conf.ConnectionLifeCycleListener != null)
            {
                this.conf.ConnectionLifeCycleListener.OnBeforeConnect();
            }
            List<string> topicInfo = this.conf.GetTopicInfo();
            if ((topicInfo == null) || (topicInfo.Count == 0))
            {
                throw new Exception("topic info is empty");
            }
            this.conf.SetTopicInfo(topicInfo);
        }

        public void OnConnect()
        {
            if (this.conf.ConnectionLifeCycleListener != null)
            {
                this.conf.ConnectionLifeCycleListener.OnConnect();
            }
        }

        public void OnConnectError(Exception e)
        {
            if (this.conf.ConnectionLifeCycleListener != null)
            {
                this.conf.ConnectionLifeCycleListener.OnConnectError(e);
            }
        }

        public void OnException(Exception e)
        {
            if (this.conf.ConnectionLifeCycleListener != null)
            {
                this.conf.ConnectionLifeCycleListener.OnException(e);
            }
        }

        public void OnMaxReadTimeoutException()
        {
            if (this.conf.ConnectionLifeCycleListener != null)
            {
                this.conf.ConnectionLifeCycleListener.OnMaxReadTimeoutException();
            }
        }

        public void OnReadTimeout()
        {
            if (this.conf.ConnectionLifeCycleListener != null)
            {
                this.conf.ConnectionLifeCycleListener.OnReadTimeout();
            }
        }

        public void OnSysErrorException(Exception e)
        {
            if (this.conf.ConnectionLifeCycleListener != null)
            {
                this.conf.ConnectionLifeCycleListener.OnSysErrorException(e);
            }
        }
    }
}

