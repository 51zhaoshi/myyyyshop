namespace Maticsoft.OAuth.Rest.Client
{
    using Maticsoft.OAuth.Http;
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class HttpServerErrorException : HttpResponseException
    {
        protected HttpServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HttpServerErrorException(Uri requestUri, HttpMethod requestMethod, HttpResponseMessage<byte[]> response) : base(requestUri, requestMethod, response)
        {
        }
    }
}

