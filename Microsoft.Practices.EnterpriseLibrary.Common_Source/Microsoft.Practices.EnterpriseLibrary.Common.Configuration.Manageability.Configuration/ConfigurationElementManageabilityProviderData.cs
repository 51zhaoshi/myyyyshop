namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using System;
    using System.ComponentModel;
    using System.Configuration;

    public class ConfigurationElementManageabilityProviderData : NameTypeConfigurationElement
    {
        private const string targetTypePropertyName = "targetType";

        public ConfigurationElementManageabilityProviderData()
        {
        }

        public ConfigurationElementManageabilityProviderData(string name, Type providerType, Type targetType) : base(name, providerType)
        {
            this.TargetType = targetType;
        }

        internal ConfigurationElementManageabilityProvider CreateManageabilityProvider()
        {
            return (ConfigurationElementManageabilityProvider) Activator.CreateInstance(base.Type);
        }

        [ConfigurationProperty("targetType", IsRequired=true), TypeConverter(typeof(AssemblyQualifiedTypeNameConverter))]
        public Type TargetType
        {
            get
            {
                return (Type) base["targetType"];
            }
            set
            {
                base["targetType"] = value;
            }
        }
    }
}

