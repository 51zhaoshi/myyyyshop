namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;

    public interface IHelperAssistedCustomConfigurationData<T> : ICustomProviderData where T: NameTypeConfigurationElement, IHelperAssistedCustomConfigurationData<T>
    {
        object BaseGetPropertyValue(ConfigurationProperty property);
        bool BaseIsModified();
        void BaseReset(ConfigurationElement parentElement);
        void BaseSetPropertyValue(ConfigurationProperty property, object value);
        void BaseUnmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode);

        CustomProviderDataHelper<T> Helper { get; }
    }
}

