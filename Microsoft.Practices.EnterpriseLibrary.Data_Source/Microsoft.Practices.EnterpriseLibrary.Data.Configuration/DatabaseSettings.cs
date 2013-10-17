namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Configuration;

    public class DatabaseSettings : SerializableConfigurationSection
    {
        private const string dbProviderMappingsProperty = "providerMappings";
        private const string defaultDatabaseProperty = "defaultDatabase";
        public const string SectionName = "dataConfiguration";

        public static DatabaseSettings GetDatabaseSettings(IConfigurationSource configurationSource)
        {
            return (DatabaseSettings) configurationSource.GetSection("dataConfiguration");
        }

        [ConfigurationProperty("defaultDatabase", IsRequired=false)]
        public string DefaultDatabase
        {
            get
            {
                return (string) base["defaultDatabase"];
            }
            set
            {
                base["defaultDatabase"] = value;
            }
        }

        [ConfigurationProperty("providerMappings", IsRequired=false)]
        public NamedElementCollection<DbProviderMapping> ProviderMappings
        {
            get
            {
                return (NamedElementCollection<DbProviderMapping>) base["providerMappings"];
            }
        }
    }
}

