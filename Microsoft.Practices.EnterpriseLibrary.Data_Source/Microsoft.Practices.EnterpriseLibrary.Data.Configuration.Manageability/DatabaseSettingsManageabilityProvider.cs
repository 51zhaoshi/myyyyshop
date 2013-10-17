namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;

    internal class DatabaseSettingsManageabilityProvider : ConfigurationSectionManageabilityProviderBase<DatabaseSettings>
    {
        private static string[] DatabaseTypeNames = new string[] { typeof(SqlDatabase).AssemblyQualifiedName, typeof(OracleDatabase).AssemblyQualifiedName, typeof(GenericDatabase).AssemblyQualifiedName };
        public const string DatabaseTypePropertyName = "databaseType";
        public const string DefaultDatabasePropertyName = "defaultDatabase";
        public const string ProviderMappingsKeyName = "providerMappings";

        public DatabaseSettingsManageabilityProvider(IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders) : base(subProviders)
        {
        }

        protected override void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, DatabaseSettings configurationSection, IConfigurationSource configurationSource, string sectionKey)
        {
            contentBuilder.StartPolicy(Resources.DatabaseSettingsPolicyName, sectionKey);
            List<AdmDropDownListItem> items = new List<AdmDropDownListItem>();
            ConnectionStringsSection section = (ConnectionStringsSection) configurationSource.GetSection("connectionStrings");
            if (section != null)
            {
                foreach (ConnectionStringSettings settings in section.ConnectionStrings)
                {
                    items.Add(new AdmDropDownListItem(settings.Name, settings.Name));
                }
            }
            contentBuilder.AddDropDownListPart(Resources.DatabaseSettingsDefaultDatabasePartName, "defaultDatabase", items, configurationSection.DefaultDatabase);
            contentBuilder.EndPolicy();
            if (configurationSection.ProviderMappings.Count > 0)
            {
                contentBuilder.StartCategory(Resources.ProviderMappingsCategoryName);
                foreach (DbProviderMapping mapping in configurationSection.ProviderMappings)
                {
                    contentBuilder.StartPolicy(string.Format(CultureInfo.InvariantCulture, Resources.ProviderMappingPolicyNameTemplate, new object[] { mapping.Name }), sectionKey + @"\providerMappings\" + mapping.Name);
                    contentBuilder.AddComboBoxPart(Resources.ProviderMappingDatabaseTypePartName, "databaseType", mapping.DatabaseType.AssemblyQualifiedName, 0xff, false, DatabaseTypeNames);
                    contentBuilder.EndPolicy();
                }
                contentBuilder.EndCategory();
            }
        }

        protected override void GenerateWmiObjectsForConfigurationSection(DatabaseSettings configurationSection, ICollection<ConfigurationSetting> wmiSettings)
        {
            wmiSettings.Add(new DatabaseBlockSetting(configurationSection.DefaultDatabase));
        }

        protected override void OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationElements(DatabaseSettings configurationSection, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            List<DbProviderMapping> list = new List<DbProviderMapping>();
            IRegistryKey machineSubKey = null;
            IRegistryKey userSubKey = null;
            try
            {
                ConfigurationSectionManageabilityProvider.LoadRegistrySubKeys("providerMappings", machineKey, userKey, out machineSubKey, out userSubKey);
                foreach (DbProviderMapping mapping in configurationSection.ProviderMappings)
                {
                    IRegistryKey key3 = null;
                    IRegistryKey key4 = null;
                    try
                    {
                        ConfigurationSectionManageabilityProvider.LoadRegistrySubKeys(mapping.Name, machineSubKey, userSubKey, out key3, out key4);
                        if (!this.OverrideWithGroupPoliciesAndGenerateWmiObjectsForDbProviderMapping(mapping, readGroupPolicies, key3, key4, generateWmiObjects, wmiSettings))
                        {
                            list.Add(mapping);
                        }
                        continue;
                    }
                    finally
                    {
                        ConfigurationSectionManageabilityProvider.ReleaseRegistryKeys(new IRegistryKey[] { key3, key4 });
                    }
                }
            }
            finally
            {
                ConfigurationSectionManageabilityProvider.ReleaseRegistryKeys(new IRegistryKey[] { machineSubKey, userSubKey });
            }
            foreach (DbProviderMapping mapping2 in list)
            {
                configurationSection.ProviderMappings.Remove(mapping2.Name);
            }
        }

        private bool OverrideWithGroupPoliciesAndGenerateWmiObjectsForDbProviderMapping(DbProviderMapping providerMapping, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            if (readGroupPolicies)
            {
                IRegistryKey key = (machineKey != null) ? machineKey : userKey;
                if (key != null)
                {
                    if (key.IsPolicyKey && !key.GetBoolValue("Available").Value)
                    {
                        return false;
                    }
                    try
                    {
                        Type typeValue = key.GetTypeValue("databaseType");
                        providerMapping.DatabaseType = typeValue;
                    }
                    catch (RegistryAccessException exception)
                    {
                        this.LogExceptionWhileOverriding(exception);
                    }
                }
            }
            if (generateWmiObjects)
            {
                wmiSettings.Add(new ProviderMappingSetting(providerMapping.DbProviderName, providerMapping.DatabaseType.AssemblyQualifiedName));
            }
            return true;
        }

        protected override void OverrideWithGroupPoliciesForConfigurationSection(DatabaseSettings configurationSection, IRegistryKey policyKey)
        {
            string stringValue = policyKey.GetStringValue("defaultDatabase");
            configurationSection.DefaultDatabase = stringValue;
        }

        protected override string SectionCategoryName
        {
            get
            {
                return Resources.DatabaseCategoryName;
            }
        }

        protected override string SectionName
        {
            get
            {
                return "dataConfiguration";
            }
        }
    }
}

