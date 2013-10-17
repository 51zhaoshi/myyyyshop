namespace Maticsoft.OAuth
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class SocialException : Exception
    {
        protected SocialException(string message) : base(message)
        {
        }

        protected SocialException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected SocialException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

