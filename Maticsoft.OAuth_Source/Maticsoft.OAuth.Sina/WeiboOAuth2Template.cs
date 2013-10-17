namespace Maticsoft.OAuth.Sina
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

    public class WeiboOAuth2Template : OAuth2Template
    {
        public WeiboOAuth2Template(string clientId, string clientSecret, string authorizeUrl, string accessTokenUrl) : base(clientId, clientSecret, authorizeUrl, accessTokenUrl, true)
        {
        }

        protected override AccessGrant CreateAccessGrant(string accessToken, string scope, string refreshToken, int? expiresIn, JsonValue response)
        {
            return new AccessGrant(accessToken, scope, refreshToken, expiresIn, new string[] { response.GetValue<string>("uid") });
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
            list.Add(new MsJsonHttpMessageConverter());
            template.MessageConverters = list;
            return template;
        }
    }
}

