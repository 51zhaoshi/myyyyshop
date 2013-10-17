namespace Maticsoft.OAuth.Rest.Client
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using System;

    public class RestOperationCanceler
    {
        private HttpMethod method;
        private IClientHttpRequest request;
        private System.Uri uri;

        internal RestOperationCanceler(IClientHttpRequest request)
        {
            this.request = request;
        }

        internal RestOperationCanceler(System.Uri uri, HttpMethod method)
        {
            this.uri = uri;
            this.method = method;
        }

        public void Cancel()
        {
            if (this.request != null)
            {
                this.request.CancelAsync();
            }
        }

        public HttpMethod Method
        {
            get
            {
                if (this.request == null)
                {
                    return this.method;
                }
                return this.request.Method;
            }
        }

        public System.Uri Uri
        {
            get
            {
                if (this.request == null)
                {
                    return this.uri;
                }
                return this.request.Uri;
            }
        }
    }
}

