namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;

    public interface IProtectedConfigurationSource
    {
        void Add(IConfigurationParameter saveParameter, string sectionName, ConfigurationSection configurationSection, string protectionProviderName);
    }
}

