namespace Maticsoft.OAuth.Json
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class JsonException : Exception
    {
        public JsonException()
        {
        }

        public JsonException(string message) : base(message)
        {
        }

        protected JsonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public JsonException(string message, Exception rootCause) : base(message, rootCause)
        {
        }
    }
}

