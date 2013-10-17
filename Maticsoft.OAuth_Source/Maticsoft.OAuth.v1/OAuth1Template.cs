namespace Maticsoft.OAuth.v1
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Rest.Client;
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;
    using System.Threading.Tasks;

    public class OAuth1Template : IOAuth1Operations
    {
        private Uri accessTokenUrl;
        private string authenticateUrl;
        private string authorizeUrl;
        private string consumerKey;
        private string consumerSecret;
        private Uri requestTokenUrl;
        private Maticsoft.OAuth.Rest.Client.RestTemplate restTemplate;
        private SigningSupport signingSupport;
        private OAuth1Version version;

        public OAuth1Template(string consumerKey, string consumerSecret, string requestTokenUrl, string authorizeUrl, string accessTokenUrl) : this(consumerKey, consumerSecret, requestTokenUrl, authorizeUrl, accessTokenUrl, OAuth1Version.Core10a)
        {
        }

        public OAuth1Template(string consumerKey, string consumerSecret, string requestTokenUrl, string authorizeUrl, string accessTokenUrl, OAuth1Version version) : this(consumerKey, consumerSecret, requestTokenUrl, authorizeUrl, null, accessTokenUrl, version)
        {
        }

        public OAuth1Template(string consumerKey, string consumerSecret, string requestTokenUrl, string authorizeUrl, string authenticateUrl, string accessTokenUrl) : this(consumerKey, consumerSecret, requestTokenUrl, authorizeUrl, authenticateUrl, accessTokenUrl, OAuth1Version.Core10a)
        {
        }

        public OAuth1Template(string consumerKey, string consumerSecret, string requestTokenUrl, string authorizeUrl, string authenticateUrl, string accessTokenUrl, OAuth1Version version)
        {
            ArgumentUtils.AssertNotNull(consumerKey, "consumerKey");
            ArgumentUtils.AssertNotNull(consumerSecret, "consumerSecret");
            ArgumentUtils.AssertNotNull(requestTokenUrl, "requestTokenUrl");
            ArgumentUtils.AssertNotNull(authorizeUrl, "authorizeUrl");
            ArgumentUtils.AssertNotNull(accessTokenUrl, "accessTokenUrl");
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.requestTokenUrl = new Uri(requestTokenUrl);
            this.authorizeUrl = authorizeUrl;
            this.authenticateUrl = authenticateUrl;
            this.accessTokenUrl = new Uri(accessTokenUrl);
            this.version = version;
            this.restTemplate = this.CreateRestTemplate();
            this.signingSupport = new SigningSupport();
        }

        protected virtual void AddCustomAuthorizationParameters(NameValueCollection parameters)
        {
        }

        public string BuildAuthenticateUrl(string requestToken, OAuth1Parameters parameters)
        {
            if (this.authenticateUrl != null)
            {
                return this.BuildAuthUrl(this.authenticateUrl, requestToken, parameters);
            }
            return this.BuildAuthorizeUrl(requestToken, parameters);
        }

        public string BuildAuthorizeUrl(string requestToken, OAuth1Parameters parameters)
        {
            return this.BuildAuthUrl(this.authorizeUrl, requestToken, parameters);
        }

        private string BuildAuthUrl(string baseAuthUrl, string requestToken, OAuth1Parameters parameters)
        {
            StringBuilder builder = new StringBuilder(baseAuthUrl);
            builder.Append("?oauth_token=").Append(HttpUtils.UrlEncode(requestToken));
            NameValueCollection values = parameters ?? new NameValueCollection();
            this.AddCustomAuthorizationParameters(values);
            foreach (string str in values)
            {
                string str2 = HttpUtils.UrlEncode(str);
                foreach (string str3 in values.GetValues(str))
                {
                    builder.Append('&').Append(str2).Append('=').Append(HttpUtils.UrlEncode(str3));
                }
            }
            return builder.ToString();
        }

        private HttpEntity CreateExchangeForTokenRequest(Uri tokenUrl, IDictionary<string, string> tokenParameters, NameValueCollection additionalParameters, string tokenSecret)
        {
            HttpHeaders headers = new HttpHeaders();
            headers.Add("Authorization", this.signingSupport.BuildAuthorizationHeaderValue(tokenUrl, tokenParameters, additionalParameters, this.consumerKey, this.consumerSecret, tokenSecret));
            if (additionalParameters == null)
            {
                additionalParameters = new NameValueCollection();
            }
            return new HttpEntity(additionalParameters, headers);
        }

        protected virtual OAuthToken CreateOAuthToken(string tokenValue, string tokenSecret, NameValueCollection response)
        {
            return new OAuthToken(tokenValue, tokenSecret);
        }

        protected virtual Maticsoft.OAuth.Rest.Client.RestTemplate CreateRestTemplate()
        {
            Maticsoft.OAuth.Rest.Client.RestTemplate template = new Maticsoft.OAuth.Rest.Client.RestTemplate();
            IList<IHttpMessageConverter> list = new List<IHttpMessageConverter>(1);
            FormHttpMessageConverter item = new FormHttpMessageConverter {
                SupportedMediaTypes = { MediaType.ALL }
            };
            list.Add(item);
            template.MessageConverters = list;
            return template;
        }

        public Task<OAuthToken> ExchangeForAccessTokenAsync(AuthorizedRequestToken requestToken, NameValueCollection additionalParameters)
        {
            IDictionary<string, string> tokenParameters = new Dictionary<string, string>(2);
            tokenParameters.Add("oauth_token", requestToken.Value);
            if (this.version == OAuth1Version.Core10a)
            {
                tokenParameters.Add("oauth_verifier", requestToken.Verifier);
            }
            HttpEntity request = this.CreateExchangeForTokenRequest(this.accessTokenUrl, tokenParameters, additionalParameters, requestToken.Secret);
            return this.restTemplate.PostForObjectAsync<NameValueCollection>(this.accessTokenUrl, request).ContinueWith<OAuthToken>(task => this.CreateOAuthToken(task.Result["oauth_token"], task.Result["oauth_token_secret"], task.Result), TaskContinuationOptions.ExecuteSynchronously);
        }

        public Task<OAuthToken> FetchRequestTokenAsync(string callbackUrl, NameValueCollection additionalParameters)
        {
            IDictionary<string, string> tokenParameters = new Dictionary<string, string>(1);
            if (this.version == OAuth1Version.Core10a)
            {
                tokenParameters.Add("oauth_callback", callbackUrl);
            }
            HttpEntity request = this.CreateExchangeForTokenRequest(this.requestTokenUrl, tokenParameters, additionalParameters, null);
            return this.restTemplate.PostForObjectAsync<NameValueCollection>(this.requestTokenUrl, request).ContinueWith<OAuthToken>(task => this.CreateOAuthToken(task.Result["oauth_token"], task.Result["oauth_token_secret"], task.Result), TaskContinuationOptions.ExecuteSynchronously);
        }

        protected string ConsumerKey
        {
            get
            {
                return this.consumerKey;
            }
        }

        public Maticsoft.OAuth.Rest.Client.RestTemplate RestTemplate
        {
            get
            {
                return this.restTemplate;
            }
        }

        public OAuth1Version Version
        {
            get
            {
                return this.version;
            }
        }
    }
}

