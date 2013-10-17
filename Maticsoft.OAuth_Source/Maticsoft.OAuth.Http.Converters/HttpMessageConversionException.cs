namespace Maticsoft.OAuth.Http.Converters
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class HttpMessageConversionException : Exception
    {
        public HttpMessageConversionException()
        {
        }

        public HttpMessageConversionException(string message) : base(message)
        {
        }

        protected HttpMessageConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HttpMessageConversionException(string message, Exception rootCause) : base(message, rootCause)
        {
        }
    }
}

