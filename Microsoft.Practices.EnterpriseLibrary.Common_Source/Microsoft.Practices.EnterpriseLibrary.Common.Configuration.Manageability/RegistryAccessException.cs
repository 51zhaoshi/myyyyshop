namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class RegistryAccessException : Exception, ISerializable
    {
        public RegistryAccessException()
        {
        }

        public RegistryAccessException(string message) : base(message)
        {
        }

        protected RegistryAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RegistryAccessException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

