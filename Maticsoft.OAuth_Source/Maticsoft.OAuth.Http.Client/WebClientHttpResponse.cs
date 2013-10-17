namespace Maticsoft.OAuth.Http.Client
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Util;
    using System;
    using System.IO;
    using System.Net;

    public class WebClientHttpResponse : IClientHttpResponse, IHttpInputMessage, IDisposable
    {
        private HttpHeaders headers;
        private System.Net.HttpWebResponse httpWebResponse;

        public WebClientHttpResponse(System.Net.HttpWebResponse response)
        {
            ArgumentUtils.AssertNotNull(response, "response");
            this.httpWebResponse = response;
            this.headers = new HttpHeaders();
            this.Initialize();
        }

        public void Close()
        {
            this.httpWebResponse.Close();
        }

        protected virtual void Initialize()
        {
            foreach (string str in this.httpWebResponse.Headers)
            {
                this.headers[str] = this.httpWebResponse.Headers[str];
            }
        }

        void IDisposable.Dispose()
        {
            ((IDisposable) this.httpWebResponse).Dispose();
        }

        public Stream Body
        {
            get
            {
                return this.httpWebResponse.GetResponseStream();
            }
        }

        public HttpHeaders Headers
        {
            get
            {
                return this.headers;
            }
        }

        public System.Net.HttpWebResponse HttpWebResponse
        {
            get
            {
                return this.httpWebResponse;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this.httpWebResponse.StatusCode;
            }
        }

        public string StatusDescription
        {
            get
            {
                return this.httpWebResponse.StatusDescription;
            }
        }
    }
}

