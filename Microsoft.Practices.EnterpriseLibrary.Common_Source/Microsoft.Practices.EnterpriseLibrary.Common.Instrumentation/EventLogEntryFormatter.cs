namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.Text;

    public class EventLogEntryFormatter : IEventLogEntryFormatter
    {
        private string applicationName;
        private string blockName;
        private static readonly string[] emptyExtraInformation = new string[0];

        public EventLogEntryFormatter(string blockName) : this(GetApplicationName(), blockName)
        {
        }

        public EventLogEntryFormatter(string applicationName, string blockName)
        {
            this.applicationName = applicationName;
            this.blockName = blockName;
        }

        private string BuildEntryText(string message, Exception exception, string[] extraInformation)
        {
            StringBuilder builder = new StringBuilder(string.Format(Resources.Culture, Resources.EventLogEntryHeaderTemplate, new object[] { this.applicationName, this.blockName }));
            builder.AppendLine();
            builder.AppendLine(message);
            for (int i = 0; i < extraInformation.Length; i++)
            {
                builder.AppendLine(extraInformation[i]);
            }
            if (exception != null)
            {
                builder.AppendLine(string.Format(Resources.Culture, Resources.EventLogEntryExceptionTemplate, new object[] { exception.ToString() }));
            }
            return builder.ToString();
        }

        private static string GetApplicationName()
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }

        public string GetEntryText(string message, params string[] extraInformation)
        {
            return this.BuildEntryText(message, null, extraInformation);
        }

        public string GetEntryText(string message, Exception exception, params string[] extraInformation)
        {
            return this.BuildEntryText(message, exception, extraInformation);
        }

        private string EntryTemplate
        {
            get
            {
                return "";
            }
        }
    }
}

