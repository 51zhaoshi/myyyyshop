namespace Maticsoft.OAuth.Rest.Client
{
    using Maticsoft.OAuth.Http;
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;

    [Serializable]
    public class HttpResponseException : RestClientException
    {
        protected static readonly Encoding DEFAULT_CHARSET = new UTF8Encoding(false);
        private HttpMethod requestMethod;
        private Uri requestUri;
        private HttpResponseMessage<byte[]> response;

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                this.requestUri = (Uri) info.GetValue("RequestUri", typeof(Uri));
                this.requestMethod = (HttpMethod) info.GetValue("RequestMethod", typeof(HttpMethod));
                this.response = (HttpResponseMessage<byte[]>) info.GetValue("Response", typeof(HttpResponseMessage<byte[]>));
            }
        }

        public HttpResponseException(Uri requestUri, HttpMethod requestMethod, HttpResponseMessage<byte[]> response) : base(string.Format("{0} request for '{1}' resulted in {2:d} - {2} ({3}).", new object[] { requestMethod, requestUri, response.StatusCode, response.StatusDescription }))
        {
            this.requestUri = requestUri;
            this.requestMethod = requestMethod;
            this.response = response;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info != null)
            {
                info.AddValue("RequestUri", this.requestUri);
                info.AddValue("RequestMethod", this.requestMethod);
                info.AddValue("Response", this.response);
            }
        }

        public string GetResponseBodyAsString()
        {
            if (this.response.Body == null)
            {
                return null;
            }
            MediaType contentType = this.response.Headers.ContentType;
            Encoding encoding = ((contentType != null) && (contentType.CharSet != null)) ? contentType.CharSet : DEFAULT_CHARSET;
            return encoding.GetString(this.response.Body, 0, this.response.Body.Length);
        }

        public HttpMethod RequestMethod
        {
            get
            {
                return this.requestMethod;
            }
        }

        public Uri RequestUri
        {
            get
            {
                return this.requestUri;
            }
        }

        public HttpResponseMessage<byte[]> Response
        {
            get
            {
                return this.response;
            }
        }
    }
}

