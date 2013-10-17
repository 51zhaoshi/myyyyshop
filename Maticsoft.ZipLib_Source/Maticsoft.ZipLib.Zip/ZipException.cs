namespace Maticsoft.ZipLib.Zip
{
    using Maticsoft.ZipLib;
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ZipException : SharpZipBaseException
    {
        public ZipException()
        {
        }

        public ZipException(string message) : base(message)
        {
        }

        protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ZipException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}

