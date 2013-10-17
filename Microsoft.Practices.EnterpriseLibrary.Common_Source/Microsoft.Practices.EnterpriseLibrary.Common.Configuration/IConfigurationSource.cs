namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;

    public interface IConfigurationSource
    {
        void Add(IConfigurationParameter saveParameter, string sectionName, ConfigurationSection configurationSection);
        void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler);
        ConfigurationSection GetSection(string sectionName);
        void Remove(IConfigurationParameter removeParameter, string sectionName);
        void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler);
    }
}

