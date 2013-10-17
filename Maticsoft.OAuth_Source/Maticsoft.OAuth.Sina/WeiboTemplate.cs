namespace Maticsoft.OAuth.Sina
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
        private AccessGrant _accessGrant;
        private static readonly Uri API_URI_BASE = new Uri("https://api.weibo.com/2/");
        private const string PROFILE_URL = "users/show.json?uid={uid}&access_token={access_token}";
        private const string STATUSES_URL = "statuses/{method}.json";

        public WeiboTemplate(AccessGrant accessGrant) : base(accessGrant)
        {
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
                throw new ArgumentNullException("No uid");
            }
            IDictionary<string, object> uriVariables = new Dictionary<string, object>();
            uriVariables.Add("uid", this._accessGrant.ExtraData[0]);
            uriVariables.Add("access_token", this._accessGrant.AccessToken);
            return base.RestTemplate.GetForObjectAsync<JsonValue>("users/show.json?uid={uid}&access_token={access_token}", uriVariables);
        }

        public Task UpdateStatusAsync(string status)
        {
            NameValueCollection request = new NameValueCollection(1);
            request.Add("access_token", this._accessGrant.AccessToken);
            request.Add("status", status);
            return base.RestTemplate.PostForMessageAsync("statuses/{method}.json", request, new object[] { "update" });
        }

        public Task UploadStatusAsync(string status, FileInfo fileInfo)
        {
            IDictionary<string, object> request = new Dictionary<string, object>();
            request.Add("access_token", this._accessGrant.AccessToken);
            request.Add("status", status);
            request.Add("pic", fileInfo);
            return base.RestTemplate.PostForObjectAsync<JsonValue>("statuses/{method}.json", request, new object[] { "upload" });
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

