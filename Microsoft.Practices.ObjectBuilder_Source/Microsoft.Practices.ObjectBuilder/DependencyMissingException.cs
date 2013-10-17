namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class DependencyMissingException : Exception
    {
        public DependencyMissingException()
        {
        }

        public DependencyMissingException(string message) : base(message)
        {
        }

        protected DependencyMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DependencyMissingException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}

