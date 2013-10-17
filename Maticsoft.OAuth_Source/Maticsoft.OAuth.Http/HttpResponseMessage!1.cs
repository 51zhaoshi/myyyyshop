namespace Maticsoft.OAuth.Http
{
    using System;
    using System.Net;

    [Serializable]
    public class HttpResponseMessage<T> : HttpResponseMessage where T: class
    {
        private T body;

        public HttpResponseMessage(T body, HttpStatusCode statusCode, string statusDescription) : this(body, new HttpHeaders(), statusCode, statusDescription)
        {
        }

        public HttpResponseMessage(T body, HttpHeaders headers, HttpStatusCode statusCode, string statusDescription) : base(headers, statusCode, statusDescription)
        {
            this.body = body;
        }

        public T Body
        {
            get
            {
                return this.body;
            }
        }
    }
}

