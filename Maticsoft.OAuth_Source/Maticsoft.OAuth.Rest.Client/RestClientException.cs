namespace Maticsoft.OAuth.Rest.Client
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class RestClientException : Exception
    {
        public RestClientException()
        {
        }

        public RestClientException(string message) : base(message)
        {
        }

        protected RestClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RestClientException(string message, Exception rootCause) : base(message, rootCause)
        {
        }
    }
}

