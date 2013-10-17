namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;

    [Serializable]
    public class ConfigurationFileChangedEventArgs : ConfigurationChangedEventArgs
    {
        private readonly string configurationFile;

        public ConfigurationFileChangedEventArgs(string configurationFile, string sectionName) : base(sectionName)
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

