namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;

    [Serializable]
    public class ConfigurationFileChangingEventArgs : ConfigurationChangingEventArgs
    {
        private readonly string configurationFile;

        public ConfigurationFileChangingEventArgs(string configurationFile, string sectionName, object oldValue, object newValue) : base(sectionName, oldValue, newValue)
        {
            this.configurationFile = configurationFile;
        }

        public string ConfigurationFile
        {
            get
            {
                return this.configurationFile;
            }
        }
    }
}

