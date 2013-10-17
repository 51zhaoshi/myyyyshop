namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
    using System;
    using System.Diagnostics;

    [CustomFactory(typeof(DefaultDataEventLoggerCustomFactory)), EventLogDefinition("Application", "Enterprise Library Data")]
    public class DefaultDataEventLogger : InstrumentationListener
    {
        private readonly IEventLogEntryFormatter eventLogEntryFormatter;

        public DefaultDataEventLogger(bool eventLoggingEnabled, bool wmiEnabled) : base((string) null, false, eventLoggingEnabled, wmiEnabled, null)
        {
            this.eventLogEntryFormatter = new EventLogEntryFormatter(Resources.BlockName);
        }

        public void LogConfigurationError(Exception exception, string instanceName)
        {
            if (base.WmiEnabled)
            {
                base.FireManagementInstrumentation(new DataConfigurationFailureEvent(instanceName, exception.ToString()));
            }
            if (base.EventLoggingEnabled)
            {
                string message = string.Format(Resources.Culture, Resources.ConfigurationFailureCreatingDatabase, new object[] { instanceName });
                string str2 = this.eventLogEntryFormatter.GetEntryText(message, exception, new string[0]);
                EventLog.WriteEntry(base.GetEventSourceName(), str2, EventLogEntryType.Error);
            }
        }
    }
}

