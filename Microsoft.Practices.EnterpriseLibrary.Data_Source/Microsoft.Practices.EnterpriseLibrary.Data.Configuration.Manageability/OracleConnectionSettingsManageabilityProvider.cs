namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability.Properties;
    using Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class OracleConnectionSettingsManageabilityProvider : ConfigurationSectionManageabilityProviderBase<OracleConnectionSettings>
    {
        public const string PackagesPropertyName = "packages";

        public OracleConnectionSettingsManageabilityProvider(IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders) : base(subProviders)
        {
        }

        protected override void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, OracleConnectionSettings configurationSection, IConfigurationSource configurationSource, string sectionKey)
        {
            contentBuilder.StartCategory(Resources.OracleConnectionsCategoryName);
            foreach (OracleConnectionData data in configurationSection.OracleConnectionsData)
            {
                string policyKey = sectionKey + @"\" + data.Name;
                contentBuilder.StartPolicy(string.Format(CultureInfo.InvariantCulture, Resources.OracleConnectionPolicyNameTemplate, new object[] { data.Name }), policyKey);
                contentBuilder.AddEditTextPart(Resources.OracleConnectionPackagesPartName, "packages", GenerateRulesString(data.Packages), 0x400, true);
                contentBuilder.EndPolicy();
            }
            contentBuilder.EndCategory();
        }

        private static string[] GeneratePackagesArray(NamedElementCollection<OraclePackageData> packages)
        {
            string[] strArray = new string[packages.Count];
            int num = 0;
            foreach (OraclePackageData data in packages)
            {
                strArray[num++] = KeyValuePairEncoder.EncodeKeyValuePair(data.Name, data.Prefix);
            }
            return strArray;
        }

        private static string GenerateRulesString(NamedElementCollection<OraclePackageData> packages)
        {
            KeyValuePairEncoder encoder = new KeyValuePairEncoder();
            foreach (OraclePackageData data in packages)
            {
                encoder.AppendKeyValuePair(data.Name, data.Prefix);
            }
            return encoder.GetEncodedKeyValuePairs();
        }

        protected override void GenerateWmiObjectsForConfigurationSection(OracleConnectionSettings configurationSection, ICollection<ConfigurationSetting> wmiSettings)
        {
        }

        protected override void OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationElements(OracleConnectionSettings configurationSection, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            List<OracleConnectionData> list = new List<OracleConnectionData>();
            foreach (OracleConnectionData data in configurationSection.OracleConnectionsData)
            {
                IRegistryKey machineSubKey = null;
                IRegistryKey userSubKey = null;
                try
                {
                    ConfigurationSectionManageabilityProvider.LoadRegistrySubKeys(data.Name, machineKey, userKey, out machineSubKey, out userSubKey);
                    if (!this.OverrideWithGroupPoliciesAndGenerateWmiObjectsForOracleConnection(data, readGroupPolicies, machineSubKey, userSubKey, generateWmiObjects, wmiSettings))
                    {
                        list.Add(data);
                    }
                    continue;
                }
                finally
                {
                    ConfigurationSectionManageabilityProvider.ReleaseRegistryKeys(new IRegistryKey[] { machineSubKey, userSubKey });
                }
            }
            foreach (OracleConnectionData data2 in list)
            {
                configurationSection.OracleConnectionsData.Remove(data2.Name);
            }
        }

        private bool OverrideWithGroupPoliciesAndGenerateWmiObjectsForOracleConnection(OracleConnectionData connectionData, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
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
                        string stringValue = key.GetStringValue("packages");
                        connectionData.Packages.Clear();
                        Dictionary<string, string> attributesDictionary = new Dictionary<string, string>();
                        KeyValuePairParser.ExtractKeyValueEntries(stringValue, attributesDictionary);
                        foreach (KeyValuePair<string, string> pair in attributesDictionary)
                        {
                            connectionData.Packages.Add(new OraclePackageData(pair.Key, pair.Value));
                        }
                    }
                    catch (RegistryAccessException exception)
                    {
                        this.LogExceptionWhileOverriding(exception);
                    }
                }
            }
            if (generateWmiObjects)
            {
                string[] packages = GeneratePackagesArray(connectionData.Packages);
                wmiSettings.Add(new OracleConnectionSetting(connectionData.Name, packages));
            }
            return true;
        }

        protected override void OverrideWithGroupPoliciesForConfigurationSection(OracleConnectionSettings configurationSection, IRegistryKey policyKey)
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
                return "oracleConnectionSettings";
            }
        }
    }
}

