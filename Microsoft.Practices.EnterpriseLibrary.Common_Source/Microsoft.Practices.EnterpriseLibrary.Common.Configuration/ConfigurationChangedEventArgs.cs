namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;

    [Serializable]
    public class ConfigurationChangedEventArgs : EventArgs
    {
        private readonly string sectionName;

        public ConfigurationChangedEventArgs(string sectionName)
        {
            this.sectionName = sectionName;
        }

        public string SectionName
        {
            get
            {
                return this.sectionName;
            }
        }
    }
}

