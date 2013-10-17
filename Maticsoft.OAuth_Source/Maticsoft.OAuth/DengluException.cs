namespace Maticsoft.OAuth
{
    using System;

    public class DengluException : Exception
    {
        private int errorCode;
        private string errorDescription;

        public DengluException(int errorCode, string errorDescription)
        {
            this.errorCode = errorCode;
            this.errorDescription = errorDescription;
        }

        public int ErrorCode
        {
            get
            {
                return this.errorCode;
            }
            set
            {
                this.errorCode = value;
            }
        }

        public string ErrorDescription
        {
            get
            {
                return this.errorDescription;
            }
            set
            {
                this.errorDescription = value;
            }
        }
    }
}

