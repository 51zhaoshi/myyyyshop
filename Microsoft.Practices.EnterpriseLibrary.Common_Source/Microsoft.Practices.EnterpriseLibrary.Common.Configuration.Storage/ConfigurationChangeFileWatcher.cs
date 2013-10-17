namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Storage
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;
    using System.IO;

    [HasInstallableResources, EventLogDefinition("Application", "Enterprise Library Configuration")]
    public class ConfigurationChangeFileWatcher : ConfigurationChangeWatcher, IConfigurationChangeWatcher, IDisposable
    {
        private string configFilePath;
        private string configurationSectionName;
        private const string eventSourceName = "Enterprise Library Configuration";

        public ConfigurationChangeFileWatcher(string configFilePath, string configurationSectionName)
        {
            if (string.IsNullOrEmpty(configFilePath))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "configFilePath");
            }
            if (configurationSectionName == null)
            {
                throw new ArgumentNullException("configurationSectionName");
            }
            this.configurationSectionName = configurationSectionName;
            this.configFilePath = configFilePath;
        }

        protected override ConfigurationChangedEventArgs BuildEventData()
        {
            return new ConfigurationFileChangedEventArgs(Path.GetFullPath(this.configFilePath), this.configurationSectionName);
        }

        protected override string BuildThreadName()
        {
            return ("_ConfigurationFileWatherThread : " + this.configFilePath);
        }

        ~ConfigurationChangeFileWatcher()
        {
            this.Disposing(false);
        }

        protected override DateTime GetCurrentLastWriteTime()
        {
            if (File.Exists(this.configFilePath))
            {
                return File.GetLastWriteTime(this.configFilePath);
            }
            return DateTime.MinValue;
        }

        protected override string GetEventSourceName()
        {
            return "Enterprise Library Configuration";
        }

        public override string SectionName
        {
            get
            {
                return this.configurationSectionName;
            }
        }
    }
}

