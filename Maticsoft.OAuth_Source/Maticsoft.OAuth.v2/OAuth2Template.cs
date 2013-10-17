namespace Maticsoft.OAuth.v2
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Http.Client.Interceptor;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Http.Converters.Json;
    using Maticsoft.OAuth.Json;
    using Maticsoft.OAuth.Rest.Client;
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;
    using System.Threading.Tasks;

    public class OAuth2Template : IOAuth2Operations
    {
        private string accessTokenUrl;
        private string authenticateUrl;
        private string authorizeUrl;
        private string clientId;
        private string clientSecret;
        private Maticsoft.OAuth.Rest.Client.RestTemplate restTemplate;
        private bool useParametersForClientAuthentication;

        public OAuth2Template(string clientId, string clientSecret, string authorizeUrl, string accessTokenUrl) : this(clientId, clientSecret, authorizeUrl, null, accessTokenUrl, false)
        {
        }

        public OAuth2Template(string clientId, string clientSecret, string authorizeUrl, string accessTokenUrl, bool useParametersForClientAuthentication) : this(clientId, clientSecret, authorizeUrl, null, accessTokenUrl, useParametersForClientAuthentication)
        {
        }

        public OAuth2Template(string clientId, string clientSecret, string authorizeUrl, string authenticateUrl, string accessTokenUrl) : this(clientId, clientSecret, authorizeUrl, authenticateUrl, accessTokenUrl, false)
        {
        }

        public OAuth2Template(string clientId, string clientSecret, string authorizeUrl, string authenticateUrl, string accessTokenUrl, bool useParametersForClientAuthentication)
        {
            ArgumentUtils.AssertNotNull(clientId, "clientId");
            ArgumentUtils.AssertNotNull(clientSecret, "clientSecret");
            ArgumentUtils.AssertNotNull(authorizeUrl, "authorizeUrl");
            ArgumentUtils.AssertNotNull(accessTokenUrl, "accessTokenUrl");
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            string str = "?client_id=" + HttpUtils.UrlEncode(clientId);
            this.authorizeUrl = authorizeUrl + str;
            if (authenticateUrl != null)
            {
                this.authenticateUrl = authenticateUrl + str;
            }
            else
            {
                this.authenticateUrl = null;
            }
            this.accessTokenUrl = accessTokenUrl;
            this.restTemplate = this.CreateRestTemplate();
            this.useParametersForClientAuthentication = useParametersForClientAuthentication;
            if (!this.useParametersForClientAuthentication)
            {
                this.restTemplate.RequestInterceptors.Add(new BasicSigningRequestInterceptor(clientId, clientSecret));
            }
        }

        public Task<AccessGrant> AuthenticateClientAsync()
        {
            return this.AuthenticateClientAsync(null);
        }

        public Task<AccessGrant> AuthenticateClientAsync(string scope)
        {
            NameValueCollection request = this.CreateAuthenticateClientRequest(scope);
            return this.PostForAccessGrantAsync(this.accessTokenUrl, request);
        }

        public string BuildAuthenticateUrl(GrantType grantType, OAuth2Parameters parameters)
        {
            if (this.authenticateUrl != null)
            {
                return BuildAuthUrl(this.authenticateUrl, grantType, parameters);
            }
            return this.BuildAuthorizeUrl(grantType, parameters);
        }

        public string BuildAuthorizeUrl(GrantType grantType, OAuth2Parameters parameters)
        {
            return BuildAuthUrl(this.authorizeUrl, grantType, parameters);
        }

        private static string BuildAuthUrl(string baseAuthUrl, GrantType grantType, OAuth2Parameters parameters)
        {
            StringBuilder builder = new StringBuilder(baseAuthUrl);
            if (grantType == GrantType.AuthorizationCode)
            {
                builder.Append("&response_type=code");
            }
            else if (grantType == GrantType.ImplicitGrant)
            {
                builder.Append("&response_type=token");
            }
            if (parameters != null)
            {
                foreach (string str in parameters)
                {
                    string str2 = HttpUtils.UrlEncode(str);
                    foreach (string str3 in parameters.GetValues(str))
                    {
                        builder.AppendFormat("&{0}={1}", str2, HttpUtils.UrlEncode(str3));
                    }
                }
            }
            return builder.ToString();
        }

        protected virtual AccessGrant CreateAccessGrant(string accessToken, string scope, string refreshToken, int? expiresIn, JsonValue response)
        {
            return new AccessGrant(accessToken, scope, refreshToken, expiresIn);
        }

        private NameValueCollection CreateAuthenticateClientRequest(string scope)
        {
            NameValueCollection values = new NameValueCollection();
            if (this.useParametersForClientAuthentication)
            {
                values.Add("client_id", this.clientId);
                values.Add("client_secret", this.clientSecret);
            }
            values.Add("grant_type", "client_credentials");
            if (scope != null)
            {
                values.Add("scope", scope);
            }
            return values;
        }

        private NameValueCollection CreateExchangeCredentialsForAccessRequest(string username, string password, NameValueCollection additionalParameters)
        {
            NameValueCollection values = new NameValueCollection();
            if (this.useParametersForClientAuthentication)
            {
                values.Add("client_id", this.clientId);
                values.Add("client_secret", this.clientSecret);
            }
            values.Add("username", username);
            values.Add("password", password);
            values.Add("grant_type", "password");
            if (additionalParameters != null)
            {
                foreach (string str in additionalParameters)
                {
                    values.Add(str, additionalParameters[str]);
                }
            }
            return values;
        }

        private NameValueCollection CreateExchangeForAccessRequest(string authorizationCode, string redirectUri, NameValueCollection additionalParameters)
        {
            NameValueCollection values = new NameValueCollection();
            if (this.useParametersForClientAuthentication)
            {
                values.Add("client_id", this.clientId);
                values.Add("client_secret", this.clientSecret);
            }
            values.Add("code", authorizationCode);
            values.Add("redirect_uri", redirectUri);
            values.Add("grant_type", "authorization_code");
            if (additionalParameters != null)
            {
                foreach (string str in additionalParameters)
                {
                    values.Add(str, additionalParameters[str]);
                }
            }
            return values;
        }

        private NameValueCollection CreateRefreshAccessRequest(string refreshToken, NameValueCollection additionalParameters)
        {
            NameValueCollection values = new NameValueCollection();
            if (this.useParametersForClientAuthentication)
            {
                values.Add("client_id", this.clientId);
                values.Add("client_secret", this.clientSecret);
            }
            values.Add("refresh_token", refreshToken);
            values.Add("grant_type", "refresh_token");
            if (additionalParameters != null)
            {
                foreach (string str in additionalParameters)
                {
                    values.Add(str, additionalParameters[str]);
                }
            }
            return values;
        }

        protected virtual Maticsoft.OAuth.Rest.Client.RestTemplate CreateRestTemplate()
        {
            Maticsoft.OAuth.Rest.Client.RestTemplate template = new Maticsoft.OAuth.Rest.Client.RestTemplate();
            ((WebClientHttpRequestFactory) template.RequestFactory).Expect100Continue = false;
            IList<IHttpMessageConverter> list = new List<IHttpMessageConverter>(2);
            FormHttpMessageConverter item = new FormHttpMessageConverter {
                SupportedMediaTypes = { MediaType.ALL }
            };
            list.Add(item);
            list.Add(new SpringJsonHttpMessageConverter());
            template.MessageConverters = list;
            return template;
        }

        public Task<AccessGrant> ExchangeCredentialsForAccessAsync(string username, string password, NameValueCollection additionalParameters)
        {
            NameValueCollection request = this.CreateExchangeCredentialsForAccessRequest(username, password, additionalParameters);
            return this.PostForAccessGrantAsync(this.accessTokenUrl, request);
        }

        public Task<AccessGrant> ExchangeForAccessAsync(string authorizationCode, string redirectUri, NameValueCollection additionalParameters)
        {
            NameValueCollection request = this.CreateExchangeForAccessRequest(authorizationCode, redirectUri, additionalParameters);
            return this.PostForAccessGrantAsync(this.accessTokenUrl, request);
        }

        private AccessGrant ExtractAccessGrant(JsonValue response)
        {
            string accessToken = response.GetValue<string>("access_token");
            string valueOrDefault = response.GetValueOrDefault<string>("scope");
            string refreshToken = response.GetValueOrDefault<string>("refresh_token");
            int? expiresIn = null;
            try
            {
                expiresIn = response.GetValueOrDefault<int?>("expires_in");
            }
            catch (JsonException)
            {
            }
            return this.CreateAccessGrant(accessToken, valueOrDefault, refreshToken, expiresIn, response);
        }

        protected virtual Task<AccessGrant> PostForAccessGrantAsync(string accessTokenUrl, NameValueCollection request)
        {
            return this.restTemplate.PostForObjectAsync<JsonValue>(accessTokenUrl, request, new object[0]).ContinueWith<AccessGrant>(task => this.ExtractAccessGrant(task.Result), TaskContinuationOptions.ExecuteSynchronously);
        }

        public Task<AccessGrant> RefreshAccessAsync(string refreshToken, NameValueCollection additionalParameters)
        {
            NameValueCollection request = this.CreateRefreshAccessRequest(refreshToken, additionalParameters);
            return this.PostForAccessGrantAsync(this.accessTokenUrl, request);
        }

        [Obsolete("Use the other RefreshAccessAsync method instead. Set the scope via additional parameters if needed.")]
        public Task<AccessGrant> RefreshAccessAsync(string refreshToken, string scope, NameValueCollection additionalParameters)
        {
            if (scope != null)
            {
                if (additionalParameters == null)
                {
                    additionalParameters = new NameValueCollection();
                }
                additionalParameters.Set("scope", scope);
            }
            return this.RefreshAccessAsync(refreshToken, additionalParameters);
        }

        public Maticsoft.OAuth.Rest.Client.RestTemplate RestTemplate
        {
            get
            {
                return this.restTemplate;
            }
        }

        public bool UseParametersForClientAuthentication
        {
            get
            {
                return this.useParametersForClientAuthentication;
            }
        }
    }
}

