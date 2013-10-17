namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class IncompatibleTypesException : Exception
    {
        public IncompatibleTypesException()
        {
        }

        public IncompatibleTypesException(string message) : base(message)
        {
        }

        protected IncompatibleTypesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IncompatibleTypesException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}

