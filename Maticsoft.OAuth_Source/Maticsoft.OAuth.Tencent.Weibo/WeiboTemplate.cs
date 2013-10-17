namespace Maticsoft.OAuth.Tencent.Weibo
{
    using Maticsoft.OAuth;
    using Maticsoft.OAuth.Http.Converters.Json;
    using Maticsoft.OAuth.Rest.Client;
    using Maticsoft.OAuth.v2;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Threading.Tasks;

    public class WeiboTemplate : AbstractOAuth2ApiBinding, IWeibo, IApiBinding
    {
        protected AccessGrant _accessGrant;
        protected string _clientId;
        internal static readonly Uri API_URI_BASE = new Uri("https://open.t.qq.com/");
        internal const string PROFILE_URL = "api/user/info?openid={openid}&clientip={clientip}&access_token={access_token}&oauth_consumer_key={oauth_consumer_key}&oauth_version={oauth_version}&scope={scope}";
        private const string STATUSES_URL = "api/t/{method}";

        public WeiboTemplate(AccessGrant accessGrant, string clientId) : base(accessGrant)
        {
            this._clientId = clientId;
            this._accessGrant = accessGrant;
        }

        protected override void ConfigureRestTemplate(RestTemplate restTemplate)
        {
            restTemplate.BaseAddress = API_URI_BASE;
        }

        protected override IList<IHttpMessageConverter> GetMessageConverters()
        {
            IList<IHttpMessageConverter> messageConverters = base.GetMessageConverters();
            messageConverters.Add(new MsJsonHttpMessageConverter());
            return messageConverters;
        }

        protected override OAuth2Version GetOAuth2Version()
        {
            return OAuth2Version.Bearer;
        }

        public Task<JsonValue> GetUserProfileAsync()
        {
            if ((this._accessGrant.ExtraData == null) || (this._accessGrant.ExtraData.Length < 3))
            {
                throw new ArgumentNullException("No openid/openkey/clientip");
            }
            IDictionary<string, object> uriVariables = new Dictionary<string, object>();
            uriVariables.Add("openid", this._accessGrant.ExtraData[0]);
            uriVariables.Add("access_token", this._accessGrant.AccessToken);
            uriVariables.Add("oauth_consumer_key", this._clientId);
            uriVariables.Add("clientip", this._accessGrant.ExtraData[2]);
            uriVariables.Add("oauth_version", "2.a");
            uriVariables.Add("scope", "all");
            return base.RestTemplate.GetForObjectAsync<JsonValue>("api/user/info?openid={openid}&clientip={clientip}&access_token={access_token}&oauth_consumer_key={oauth_consumer_key}&oauth_version={oauth_version}&scope={scope}", uriVariables);
        }

        public Task UpdateStatusAsync(string status)
        {
            NameValueCollection request = new NameValueCollection(1);
            request.Add("openid", this._accessGrant.ExtraData[0]);
            request.Add("access_token", this._accessGrant.AccessToken);
            request.Add("oauth_consumer_key", this._clientId);
            request.Add("content", status);
            request.Add("clientip", this._accessGrant.ExtraData[2]);
            request.Add("oauth_version", "2.a");
            request.Add("scope", "all");
            return base.RestTemplate.PostForMessageAsync("api/t/{method}", request, new object[] { "add" });
        }

        public Task UploadStatusAsync(string status, FileInfo fileInfo)
        {
            IDictionary<string, object> request = new Dictionary<string, object>();
            request.Add("openid", this._accessGrant.ExtraData[0]);
            request.Add("access_token", this._accessGrant.AccessToken);
            request.Add("oauth_consumer_key", this._clientId);
            request.Add("content", status);
            request.Add("pic", fileInfo);
            request.Add("clientip", this._accessGrant.ExtraData[2]);
            request.Add("oauth_version", "2.a");
            request.Add("scope", "all");
            return base.RestTemplate.PostForObjectAsync<JsonValue>("api/t/{method}", request, new object[] { "add_pic" });
        }

        public IRestOperations RestOperations
        {
            get
            {
                return base.RestTemplate;
            }
        }
    }
}

