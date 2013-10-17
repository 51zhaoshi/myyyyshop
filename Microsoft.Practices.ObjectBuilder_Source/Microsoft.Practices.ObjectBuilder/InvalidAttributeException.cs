namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidAttributeException : Exception
    {
        public InvalidAttributeException()
        {
        }

        public InvalidAttributeException(string message) : base(message)
        {
        }

        protected InvalidAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidAttributeException(string message, Exception exception) : base(message, exception)
        {
        }

        public InvalidAttributeException(Type type, string memberName) : base(string.Format(CultureInfo.CurrentCulture, Resources.InvalidAttributeCombination, new object[] { type, memberName }))
        {
        }
    }
}

