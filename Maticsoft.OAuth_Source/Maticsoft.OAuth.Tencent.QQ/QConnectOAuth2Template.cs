namespace Maticsoft.OAuth.Tencent.QQ
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Http.Converters.Json;
    using Maticsoft.OAuth.Json;
    using Maticsoft.OAuth.Rest.Client;
    using Maticsoft.OAuth.v2;
    using System;
    using System.Collections.Generic;

    public class QConnectOAuth2Template : OAuth2Template
    {
        public QConnectOAuth2Template(string clientId, string clientSecret, string authorizeUrl, string accessTokenUrl) : base(clientId, clientSecret, authorizeUrl, accessTokenUrl, true)
        {
        }

        protected override AccessGrant CreateAccessGrant(string accessToken, string scope, string refreshToken, int? expiresIn, JsonValue response)
        {
            string url = new Uri(QConnectTemplate.API_URI_BASE, "oauth2.0/me?access_token={access_token}").ToString();
            IDictionary<string, object> uriVariables = new Dictionary<string, object>();
            uriVariables.Add("access_token", accessToken);
            QConnectOpenIdTemplate template = new QConnectOpenIdTemplate(new AccessGrant(accessToken));
            JsonValue result = template.RestTemplate.GetForObjectAsync<JsonValue>(url, uriVariables).Result;
            return new AccessGrant(accessToken, scope, refreshToken, expiresIn, new string[] { result.GetValue<string>("openid") });
        }

        protected override RestTemplate CreateRestTemplate()
        {
            RestTemplate template = new RestTemplate();
            ((WebClientHttpRequestFactory) template.RequestFactory).Expect100Continue = false;
            IList<IHttpMessageConverter> list = new List<IHttpMessageConverter>(2);
            FormHttpMessageConverter item = new FormHttpMessageConverter {
                SupportedMediaTypes = { MediaType.ALL }
            };
            list.Add(item);
            list.Add(new TextJsonHttpMessageConverter());
            template.MessageConverters = list;
            return template;
        }
    }
}

