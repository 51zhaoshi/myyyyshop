namespace Maticsoft.Common.Mail
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class Pop3Exception : Exception
    {
        public Pop3Exception()
        {
        }

        public Pop3Exception(string message) : base(message)
        {
        }

        protected Pop3Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Pop3Exception(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

