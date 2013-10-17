namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using System;

    public class DataConfigurationFailureEvent : DataEvent
    {
        private string exceptionMessage;

        public DataConfigurationFailureEvent(string instanceName, string exceptionMessage) : base(instanceName)
        {
            this.exceptionMessage = exceptionMessage;
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

