namespace Maticsoft.Json
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class JsonException : Exception
    {
        private const string _defaultMessage = "An error occurred dealing with JSON data.";

        public JsonException() : this(null)
        {
        }

        public JsonException(string message) : base(Mask.NullString(message, "An error occurred dealing with JSON data."), null)
        {
        }

        protected JsonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public JsonException(string message, Exception innerException) : base(Mask.NullString(message, "An error occurred dealing with JSON data."), innerException)
        {
        }
    }
}

