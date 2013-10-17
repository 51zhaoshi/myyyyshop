namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class ConfigurationSectionManageabilityProviderData : NameTypeConfigurationElement
    {
        private const string manageabilityProvidersCollectionPropertyName = "manageabilityProviders";

        public ConfigurationSectionManageabilityProviderData()
        {
        }

        public ConfigurationSectionManageabilityProviderData(string sectionName, Type providerType) : base(sectionName, providerType)
        {
        }

        internal ConfigurationSectionManageabilityProvider CreateManageabilityProvider()
        {
            IDictionary<Type, ConfigurationElementManageabilityProvider> dictionary = new Dictionary<Type, ConfigurationElementManageabilityProvider>();
            foreach (ConfigurationElementManageabilityProviderData data in this.ManageabilityProviders)
            {
                ConfigurationElementManageabilityProvider provider = data.CreateManageabilityProvider();
                dictionary.Add(data.TargetType, provider);
            }
            return (ConfigurationSectionManageabilityProvider) Activator.CreateInstance(base.Type, new object[] { dictionary });
        }

        [ConfigurationProperty("manageabilityProviders")]
        public NamedElementCollection<ConfigurationElementManageabilityProviderData> ManageabilityProviders
        {
            get
            {
                return (NamedElementCollection<ConfigurationElementManageabilityProviderData>) base["manageabilityProviders"];
            }
        }
    }
}

