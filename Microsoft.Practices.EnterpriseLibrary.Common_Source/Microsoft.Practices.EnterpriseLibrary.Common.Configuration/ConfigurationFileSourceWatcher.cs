namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Storage;
    using System;
    using System.IO;

    public class ConfigurationFileSourceWatcher : ConfigurationSourceWatcher
    {
        private string configurationFilepath;

        public ConfigurationFileSourceWatcher(string configurationFilepath, string configSource, bool refresh, ConfigurationChangedEventHandler changed) : base(configSource, refresh, changed)
        {
            this.configurationFilepath = configurationFilepath;
            if (refresh)
            {
                this.SetUpWatcher(changed);
            }
        }

        public static string GetFullFileName(string configurationFilepath, string configSource)
        {
            if (string.Empty == configSource)
            {
                return configurationFilepath;
            }
            if (!Path.IsPathRooted(configSource))
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configSource);
            }
            return configSource;
        }

        private void SetUpWatcher(ConfigurationChangedEventHandler changed)
        {
            base.configWatcher = new ConfigurationChangeFileWatcher(GetFullFileName(this.configurationFilepath, base.ConfigSource), base.ConfigSource);
            base.configWatcher.ConfigurationChanged += changed;
        }
    }
}

