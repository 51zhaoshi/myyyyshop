namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using System;

    public class ConnectionFailedEvent : DataEvent
    {
        private string connectionString;
        private string exceptionMessage;

        public ConnectionFailedEvent(string instanceName, string connectionString, string exceptionMessage) : base(instanceName)
        {
            this.connectionString = connectionString;
            this.exceptionMessage = exceptionMessage;
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        public string ExceptionMessage
        {
            get
            {
                return this.exceptionMessage;
            }
        }
    }
}

