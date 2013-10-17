namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;

    public class NullConfigurationSource : IConfigurationSource
    {
        public void Add(IConfigurationParameter saveParameter, string sectionName, ConfigurationSection configurationSection)
        {
        }

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
        }

        public ConfigurationSection GetSection(string sectionName)
        {
            return null;
        }

        public void Remove(IConfigurationParameter removeParameter, string sectionName)
        {
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
        }
    }
}

