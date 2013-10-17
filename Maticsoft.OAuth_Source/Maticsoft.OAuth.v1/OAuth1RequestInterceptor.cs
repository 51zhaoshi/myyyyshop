namespace Maticsoft.OAuth.v1
{
    using Maticsoft.OAuth.Http.Client.Interceptor;
    using System;

    public class OAuth1RequestInterceptor : IClientHttpRequestBeforeInterceptor, IClientHttpRequestInterceptor
    {
        private string accessToken;
        private string accessTokenSecret;
        private string consumerKey;
        private string consumerSecret;
        private SigningSupport signingSupport;

        public OAuth1RequestInterceptor(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.accessToken = accessToken;
            this.accessTokenSecret = accessTokenSecret;
            this.signingSupport = new SigningSupport();
        }

        public void BeforeExecute(IClientHttpRequestContext request)
        {
            string str = this.signingSupport.BuildAuthorizationHeaderValue(request.Uri, request.Method, request.Headers, request.Body, this.consumerKey, this.consumerSecret, this.accessToken, this.accessTokenSecret);
            request.Headers["Authorization"] = str;
        }
    }
}

