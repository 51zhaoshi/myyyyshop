namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;

    internal class ConnectionStringsManageabilityProvider : ConfigurationSectionManageabilityProviderBase<ConnectionStringsSection>
    {
        public const string ConnectionStringPropertyName = "connectionString";
        public const string ProviderNamePropertyName = "providerName";

        public ConnectionStringsManageabilityProvider(IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders) : base(subProviders)
        {
        }

        protected override void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, ConnectionStringsSection configurationSection, IConfigurationSource configurationSource, string sectionKey)
        {
            contentBuilder.StartCategory(Resources.ConnectionStringsCategoryName);
            foreach (ConnectionStringSettings settings in configurationSection.ConnectionStrings)
            {
                contentBuilder.StartPolicy(string.Format(CultureInfo.InvariantCulture, Resources.ConnectionStringPolicyNameTemplate, new object[] { settings.Name }), sectionKey + @"\" + settings.Name);
                contentBuilder.AddEditTextPart(Resources.ConnectionStringConnectionStringPartName, "connectionString", settings.ConnectionString, 500, true);
                contentBuilder.AddComboBoxPart(Resources.ConnectionStringProviderNamePartName, "providerName", settings.ProviderName, 0xff, true, new string[] { "System.Data.SqlClient", "System.Data.OracleClient" });
                contentBuilder.EndPolicy();
            }
            contentBuilder.EndCategory();
        }

        protected override void GenerateWmiObjectsForConfigurationSection(ConnectionStringsSection configurationSection, ICollection<ConfigurationSetting> wmiSettings)
        {
        }

        protected override void OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationElements(ConnectionStringsSection configurationSection, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            List<ConnectionStringSettings> list = new List<ConnectionStringSettings>();
            foreach (ConnectionStringSettings settings in configurationSection.ConnectionStrings)
            {
                IRegistryKey machineSubKey = null;
                IRegistryKey userSubKey = null;
                try
                {
                    ConfigurationSectionManageabilityProvider.LoadRegistrySubKeys(settings.Name, machineKey, userKey, out machineSubKey, out userSubKey);
                    if (!this.OverrideWithGroupPoliciesAndGenerateWmiObjectsForConnectionString(settings, readGroupPolicies, machineSubKey, userSubKey, generateWmiObjects, wmiSettings))
                    {
                        list.Add(settings);
                    }
                    continue;
                }
                finally
                {
                    ConfigurationSectionManageabilityProvider.ReleaseRegistryKeys(new IRegistryKey[] { machineSubKey, userSubKey });
                }
            }
            foreach (ConnectionStringSettings settings2 in list)
            {
                configurationSection.ConnectionStrings.Remove(settings2);
            }
        }

        private bool OverrideWithGroupPoliciesAndGenerateWmiObjectsForConnectionString(ConnectionStringSettings connectionString, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
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
                        string stringValue = key.GetStringValue("connectionString");
                        string str2 = key.GetStringValue("providerName");
                        connectionString.ConnectionString = stringValue;
                        connectionString.ProviderName = str2;
                    }
                    catch (RegistryAccessException exception)
                    {
                        this.LogExceptionWhileOverriding(exception);
                    }
                }
            }
            if (generateWmiObjects)
            {
                wmiSettings.Add(new ConnectionStringSetting(connectionString.Name, connectionString.ConnectionString, connectionString.ProviderName));
            }
            return true;
        }

        protected override void OverrideWithGroupPoliciesForConfigurationSection(ConnectionStringsSection configurationSection, IRegistryKey policyKey)
        {
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
                return "connectionStrings";
            }
        }
    }
}

