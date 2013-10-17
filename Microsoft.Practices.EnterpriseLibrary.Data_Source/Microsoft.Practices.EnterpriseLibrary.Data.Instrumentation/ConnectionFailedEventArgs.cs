namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using System;

    public class ConnectionFailedEventArgs : EventArgs
    {
        private string connectionString;
        private System.Exception exception;

        public ConnectionFailedEventArgs(string connectionString, System.Exception exception)
        {
            this.connectionString = connectionString;
            this.exception = exception;
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        public System.Exception Exception
        {
            get
            {
                return this.exception;
            }
        }
    }
}

