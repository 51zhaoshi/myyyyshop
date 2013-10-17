namespace Maticsoft.OAuth.Tencent.QQ
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

    public class QConnectTemplate : AbstractOAuth2ApiBinding, IQConnect, IApiBinding
    {
        protected AccessGrant _accessGrant;
        protected string _clientId;
        internal static readonly Uri API_URI_BASE = new Uri("https://graph.qq.com/");
        internal const string PROFILE_OPENID = "oauth2.0/me?access_token={access_token}";
        internal const string PROFILE_URL = "user/get_user_info?openid={openid}&access_token={access_token}&oauth_consumer_key={oauth_consumer_key}";
        private const string STATUSES_URL = "t/{method}";

        public QConnectTemplate(AccessGrant accessGrant, string clientId) : base(accessGrant)
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
            if ((this._accessGrant.ExtraData == null) || (this._accessGrant.ExtraData.Length < 1))
            {
                throw new ArgumentNullException("No openid/openkey");
            }
            IDictionary<string, object> uriVariables = new Dictionary<string, object>();
            uriVariables.Add("openid", this._accessGrant.ExtraData[0]);
            uriVariables.Add("access_token", this._accessGrant.AccessToken);
            uriVariables.Add("oauth_consumer_key", this._clientId);
            return base.RestTemplate.GetForObjectAsync<JsonValue>("user/get_user_info?openid={openid}&access_token={access_token}&oauth_consumer_key={oauth_consumer_key}", uriVariables);
        }

        public Task UpdateStatusAsync(string status)
        {
            NameValueCollection request = new NameValueCollection(1);
            request.Add("openid", this._accessGrant.ExtraData[0]);
            request.Add("access_token", this._accessGrant.AccessToken);
            request.Add("oauth_consumer_key", this._clientId);
            request.Add("content", status);
            return base.RestTemplate.PostForMessageAsync("t/{method}", request, new object[] { "add_t" });
        }

        public Task UploadStatusAsync(string status, FileInfo fileInfo)
        {
            IDictionary<string, object> request = new Dictionary<string, object>();
            request.Add("openid", this._accessGrant.ExtraData[0]);
            request.Add("access_token", this._accessGrant.AccessToken);
            request.Add("oauth_consumer_key", this._clientId);
            request.Add("content", status);
            request.Add("pic", fileInfo);
            return base.RestTemplate.PostForMessageAsync("t/{method}", request, new object[] { "add_pic_t" });
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

