namespace Maticsoft.ZipLib.Tar
{
    using Maticsoft.ZipLib;
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class TarException : SharpZipBaseException
    {
        public TarException()
        {
        }

        public TarException(string message) : base(message)
        {
        }

        protected TarException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TarException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}

