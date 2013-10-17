namespace Maticsoft.TaoBao.Jushita
{
    using Maticsoft.TaoBao.Stream;
    using Maticsoft.TaoBao.Stream.Connect;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class JushitaConfiguration : Configuration
    {
        private const string SERVER_URL = "http://synccenter.taobao.com/message/sub";

        public JushitaConfiguration(string appKey, string appSecret, string connectId) : base(appKey, appSecret, null, connectId)
        {
            base.SetConnectUrl("http://synccenter.taobao.com/message/sub");
            base.SetMinThreads(1);
            base.SetMaxThreads(1);
            foreach (TopCometStreamRequest request in base.GetConnectReqParam())
            {
                request.SetConnectListener(new JushitaConnectionLifeCycleListener(this));
            }
        }

        public abstract List<string> GetTopicInfo();
        public void SetTopicInfo(List<string> topicInfoList)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in topicInfoList)
            {
                if (builder.Length > 0)
                {
                    builder.Append(";");
                }
                builder.Append(str);
            }
            if (base.GetConnectReqParam().Count > 1)
            {
                throw new Exception("配置状态不正常，连接配置大于1个");
            }
            foreach (TopCometStreamRequest request in base.GetConnectReqParam())
            {
                if (request.GetOtherParam() == null)
                {
                    request.SetOtherParam(new Dictionary<string, string>());
                }
                request.GetOtherParam().Add("topic", builder.ToString());
            }
        }

        public IConnectionLifeCycleListener ConnectionLifeCycleListener { get; set; }
    }
}

