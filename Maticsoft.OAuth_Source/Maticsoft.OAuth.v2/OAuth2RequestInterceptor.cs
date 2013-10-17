namespace Maticsoft.OAuth.v2
{
    using Maticsoft.OAuth.Http.Client.Interceptor;
    using System;

    public class OAuth2RequestInterceptor : IClientHttpRequestBeforeInterceptor, IClientHttpRequestInterceptor
    {
        private string accessToken;
        private OAuth2Version oauth2Version;

        public OAuth2RequestInterceptor(string accessToken, OAuth2Version oauth2Version)
        {
            this.accessToken = accessToken;
            this.oauth2Version = oauth2Version;
        }

        public void BeforeExecute(IClientHttpRequestContext request)
        {
            switch (this.oauth2Version)
            {
                case OAuth2Version.Bearer:
                    request.Headers["Authorization"] = "Bearer " + this.accessToken;
                    return;

                case OAuth2Version.Draft10:
                    request.Headers["Authorization"] = "OAuth " + this.accessToken;
                    return;

                case OAuth2Version.Draft8:
                    request.Headers["Authorization"] = "Token token=\"" + this.accessToken + "\"";
                    return;
            }
        }
    }
}

