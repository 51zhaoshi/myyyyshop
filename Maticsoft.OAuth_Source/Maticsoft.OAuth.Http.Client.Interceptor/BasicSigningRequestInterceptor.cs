namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using System;
    using System.Text;

    public class BasicSigningRequestInterceptor : IClientHttpRequestBeforeInterceptor, IClientHttpRequestInterceptor
    {
        private string authorizationHeaderValue;

        public BasicSigningRequestInterceptor(string userName, string password)
        {
            string s = string.Format("{0}:{1}", userName, password);
            s = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
            this.authorizationHeaderValue = "Basic " + s;
        }

        public void BeforeExecute(IClientHttpRequestContext request)
        {
            request.Headers["Authorization"] = this.authorizationHeaderValue;
        }
    }
}

