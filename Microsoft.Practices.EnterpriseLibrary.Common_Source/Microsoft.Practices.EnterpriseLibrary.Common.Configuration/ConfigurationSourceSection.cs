namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Configuration;

    public class ConfigurationSourceSection : SerializableConfigurationSection
    {
        public const string SectionName = "enterpriseLibrary.ConfigurationSource";
        private const string selectedSourceProperty = "selectedSource";
        private const string sourcesProperty = "sources";

        public static ConfigurationSourceSection GetConfigurationSourceSection()
        {
            return (ConfigurationSourceSection) ConfigurationManager.GetSection("enterpriseLibrary.ConfigurationSource");
        }

        [ConfigurationProperty("selectedSource", IsRequired=true)]
        public string SelectedSource
        {
            get
            {
                return (string) base["selectedSource"];
            }
            set
            {
                base["selectedSource"] = value;
            }
        }

        [ConfigurationProperty("sources", IsRequired=true)]
        public NameTypeConfigurationElementCollection<ConfigurationSourceElement, ConfigurationSourceElement> Sources
        {
            get
            {
                return (NameTypeConfigurationElementCollection<ConfigurationSourceElement, ConfigurationSourceElement>) base["sources"];
            }
        }
    }
}

