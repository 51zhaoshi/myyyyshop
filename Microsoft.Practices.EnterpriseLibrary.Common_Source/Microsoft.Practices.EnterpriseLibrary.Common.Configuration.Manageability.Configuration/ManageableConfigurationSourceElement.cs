namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class ManageableConfigurationSourceElement : ConfigurationSourceElement
    {
        private const string applicationNamePropertyName = "applicationName";
        private const string enableGroupPoliciesPropertyName = "enableGroupPolicies";
        private const string enableWmiPropertyName = "enableWmi";
        private const string filePathPropertyName = "filePath";
        private const string manageabilityProvidersCollectionPropertyName = "manageabilityProviders";
        public const int MaximumApplicationNameLength = 0xff;
        public const int MinimumApplicationNameLength = 1;

        public ManageableConfigurationSourceElement() : base(Resources.ManageableConfigurationSourceName, typeof(ManageableConfigurationSource))
        {
        }

        public ManageableConfigurationSourceElement(string name, string filePath, string applicationName) : this(name, filePath, applicationName, true, true)
        {
        }

        public ManageableConfigurationSourceElement(string name, string filePath, string applicationName, bool enableGroupPolicies, bool enableWmi) : base(name, typeof(ManageableConfigurationSource))
        {
            this.FilePath = filePath;
            this.ApplicationName = applicationName;
            this.EnableGroupPolicies = enableGroupPolicies;
            this.EnableWmi = enableWmi;
        }

        protected internal override IConfigurationSource CreateSource()
        {
            IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders = new Dictionary<string, ConfigurationSectionManageabilityProvider>(this.ConfigurationManageabilityProviders.Count);
            foreach (ConfigurationSectionManageabilityProviderData data in this.ConfigurationManageabilityProviders)
            {
                manageabilityProviders.Add(data.Name, data.CreateManageabilityProvider());
            }
            return new ManageableConfigurationSource(this.FilePath, manageabilityProviders, this.EnableGroupPolicies, this.EnableWmi, this.ApplicationName);
        }

        internal IConfigurationSource InvokeCreateSource()
        {
            return this.CreateSource();
        }

        [StringValidator(MinLength=1, MaxLength=0xff), ConfigurationProperty("applicationName", IsRequired=true, DefaultValue="Application")]
        public string ApplicationName
        {
            get
            {
                return (string) base["applicationName"];
            }
            set
            {
                base["applicationName"] = value;
            }
        }

        [ConfigurationProperty("manageabilityProviders")]
        public NamedElementCollection<ConfigurationSectionManageabilityProviderData> ConfigurationManageabilityProviders
        {
            get
            {
                return (NamedElementCollection<ConfigurationSectionManageabilityProviderData>) base["manageabilityProviders"];
            }
        }

        [ConfigurationProperty("enableGroupPolicies", DefaultValue=true)]
        public bool EnableGroupPolicies
        {
            get
            {
                return (bool) base["enableGroupPolicies"];
            }
            set
            {
                base["enableGroupPolicies"] = value;
            }
        }

        [ConfigurationProperty("enableWmi", DefaultValue=true)]
        public bool EnableWmi
        {
            get
            {
                return (bool) base["enableWmi"];
            }
            set
            {
                base["enableWmi"] = value;
            }
        }

        [ConfigurationProperty("filePath", IsRequired=true)]
        public string FilePath
        {
            get
            {
                return (string) base["filePath"];
            }
            set
            {
                base["filePath"] = value;
            }
        }
    }
}

