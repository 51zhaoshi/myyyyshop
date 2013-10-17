namespace Maticsoft.TaoBao
{
    using System;
    using System.Runtime.Serialization;

    public class TopException : Exception
    {
        private string errorCode;
        private string errorMsg;

        public TopException()
        {
        }

        public TopException(string message) : base(message)
        {
        }

        protected TopException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TopException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TopException(string errorCode, string errorMsg) : base(errorCode + ":" + errorMsg)
        {
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }

        public string ErrorCode
        {
            get
            {
                return this.errorCode;
            }
        }

        public string ErrorMsg
        {
            get
            {
                return this.errorMsg;
            }
        }
    }
}

