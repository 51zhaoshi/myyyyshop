namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using System;

    public class CommandFailedEventArgs : EventArgs
    {
        private string commandText;
        private string connectionString;
        private System.Exception exception;

        public CommandFailedEventArgs(string commandText, string connectionString, System.Exception exception)
        {
            this.commandText = commandText;
            this.connectionString = connectionString;
            this.exception = exception;
        }

        public string CommandText
        {
            get
            {
                return this.commandText;
            }
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

