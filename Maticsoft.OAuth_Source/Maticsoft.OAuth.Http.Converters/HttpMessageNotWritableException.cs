namespace Maticsoft.OAuth.Http.Converters
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class HttpMessageNotWritableException : HttpMessageConversionException
    {
        public HttpMessageNotWritableException()
        {
        }

        public HttpMessageNotWritableException(string message) : base(message)
        {
        }

        protected HttpMessageNotWritableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HttpMessageNotWritableException(string message, Exception rootCause) : base(message, rootCause)
        {
        }
    }
}

