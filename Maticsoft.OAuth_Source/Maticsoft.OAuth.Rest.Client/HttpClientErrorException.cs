namespace Maticsoft.OAuth.Rest.Client
{
    using Maticsoft.OAuth.Http;
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class HttpClientErrorException : HttpResponseException
    {
        protected HttpClientErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HttpClientErrorException(Uri requestUri, HttpMethod requestMethod, HttpResponseMessage<byte[]> response) : base(requestUri, requestMethod, response)
        {
        }
    }
}

