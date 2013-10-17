namespace Maticsoft.OAuth.Http.Converters
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class HttpMessageNotReadableException : HttpMessageConversionException
    {
        public HttpMessageNotReadableException()
        {
        }

        public HttpMessageNotReadableException(string message) : base(message)
        {
        }

        protected HttpMessageNotReadableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HttpMessageNotReadableException(string message, Exception rootCause) : base(message, rootCause)
        {
        }
    }
}

