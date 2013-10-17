namespace Maticsoft.OAuth.Http
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Net;

    [Serializable]
    public class HttpResponseMessage
    {
        private HttpHeaders headers;
        private HttpStatusCode statusCode;
        private string statusDescription;

        public HttpResponseMessage(HttpStatusCode statusCode, string statusDescription) : this(new HttpHeaders(), statusCode, statusDescription)
        {
        }

        public HttpResponseMessage(HttpHeaders headers, HttpStatusCode statusCode, string statusDescription)
        {
            ArgumentUtils.AssertNotNull(headers, "headers");
            this.headers = headers;
            this.statusCode = statusCode;
            this.statusDescription = statusDescription;
        }

        public HttpHeaders Headers
        {
            get
            {
                return this.headers;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }
        }

        public string StatusDescription
        {
            get
            {
                return this.statusDescription;
            }
        }
    }
}

