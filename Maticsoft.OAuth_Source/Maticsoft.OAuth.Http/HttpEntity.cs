namespace Maticsoft.OAuth.Http
{
    using Maticsoft.OAuth.Util;
    using System;

    public class HttpEntity
    {
        private object body;
        private HttpHeaders headers;

        public HttpEntity() : this(null, new HttpHeaders())
        {
        }

        public HttpEntity(HttpHeaders headers) : this(null, headers)
        {
        }

        public HttpEntity(object body) : this(body, new HttpHeaders())
        {
        }

        public HttpEntity(object body, HttpHeaders headers)
        {
            ArgumentUtils.AssertNotNull(headers, "headers");
            this.body = body;
            this.headers = headers;
        }

        public object Body
        {
            get
            {
                return this.body;
            }
        }

        public bool HasBody
        {
            get
            {
                return (this.body != null);
            }
        }

        public HttpHeaders Headers
        {
            get
            {
                return this.headers;
            }
        }
    }
}

