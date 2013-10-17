namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using System;
    using System.Diagnostics;
    using System.Text;

    [HasInstallableResources, EventLogDefinition("Application", "Enterprise Library Manageability Extensions")]
    public static class ManageabilityExtensionsLogger
    {
        internal const string EventLogSourceName = "Enterprise Library Manageability Extensions";

        public static void LogException(Exception exception, string title)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(title);
            builder.Append(exception.Message);
            try
            {
                EventLog.WriteEntry("Enterprise Library Manageability Extensions", builder.ToString(), EventLogEntryType.Error);
            }
            catch
            {
            }
        }

        public static void LogExceptionWhileOverriding(Exception exception)
        {
            LogException(exception, Resources.ExceptionErrorWhileOverriding);
        }
    }
}

