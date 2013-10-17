namespace Maticsoft.TaoBao.Jushita
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;

    public class JushitaTopClient
    {
        private const string SYNC_CENTER_URL = "http://eai.taobao.com/api";
        private DefaultTopClient topClient;

        public JushitaTopClient(string appKey, string appSecret) : this("http://eai.taobao.com/api", appKey, appSecret)
        {
        }

        public JushitaTopClient(string serverUrl, string appKey, string appSecret)
        {
            this.topClient = new DefaultTopClient(serverUrl, appKey, appSecret);
            this.topClient.SetDisableParser(true);
        }

        public JushitaTopClient(string serverUrl, string appKey, string appSecret, int timeout) : this(serverUrl, appKey, appSecret)
        {
            this.topClient.SetTimeout(timeout);
        }

        public string execute(string apiName, IDictionary<string, string> parameters, string session)
        {
            JushitaRequest request = new JushitaRequest {
                ApiName = apiName,
                Parameters = parameters
            };
            return this.topClient.Execute<JushitaResponse>(request, session).Body;
        }
    }
}

