namespace Maticsoft.Json
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidMemberException : Exception
    {
        private const string _defaultMessage = "No element exists at the specified index.";

        public InvalidMemberException() : this(null)
        {
        }

        public InvalidMemberException(string message) : base(Mask.NullString(message, "No element exists at the specified index."))
        {
        }

        protected InvalidMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidMemberException(string message, Exception innerException) : base(Mask.NullString(message, "No element exists at the specified index."), innerException)
        {
        }
    }
}

