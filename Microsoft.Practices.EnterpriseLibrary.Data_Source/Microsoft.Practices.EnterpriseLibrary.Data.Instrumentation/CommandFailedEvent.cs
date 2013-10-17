namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using System;

    public class CommandFailedEvent : DataEvent
    {
        private string commandText;
        private string connectionString;
        private string exceptionMessage;

        public CommandFailedEvent(string instanceName, string connectionString, string commandText, string exceptionMessage) : base(instanceName)
        {
            this.connectionString = connectionString;
            this.commandText = commandText;
            this.exceptionMessage = exceptionMessage;
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

        public string ExceptionMessage
        {
            get
            {
                return this.exceptionMessage;
            }
        }
    }
}

