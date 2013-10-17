namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Runtime.InteropServices;

    public abstract class ConfigurationSectionManageabilityProvider
    {
        public const string PolicyValueName = "Available";
        private IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders;

        protected ConfigurationSectionManageabilityProvider(IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders)
        {
            this.subProviders = subProviders;
        }

        protected internal abstract void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, ConfigurationSection configurationObject, IConfigurationSource configurationSource, string applicationName);
        protected static void AddAdministrativeTemplateDirectivesForElement<T>(AdmContentBuilder contentBuilder, T element, ConfigurationElementManageabilityProvider subProvider, IConfigurationSource configurationSource, string parentKey) where T: NamedConfigurationElement, new()
        {
            subProvider.AddAdministrativeTemplateDirectives(contentBuilder, element, configurationSource, parentKey);
        }

        protected void AddElementsPolicies<T>(AdmContentBuilder contentBuilder, NamedElementCollection<T> elements, IConfigurationSource configurationSource, string parentKey, string categoryName) where T: NamedConfigurationElement, new()
        {
            contentBuilder.StartCategory(categoryName);
            foreach (T local in elements)
            {
                ConfigurationElementManageabilityProvider subProvider = this.GetSubProvider(local.GetType());
                if (subProvider != null)
                {
                    AddAdministrativeTemplateDirectivesForElement<T>(contentBuilder, local, subProvider, configurationSource, parentKey);
                }
            }
            contentBuilder.EndCategory();
        }

        protected static IRegistryKey GetPolicyKey(IRegistryKey machineKey, IRegistryKey userKey)
        {
            if ((machineKey != null) && machineKey.IsPolicyKey)
            {
                return machineKey;
            }
            if ((userKey != null) && userKey.IsPolicyKey)
            {
                return userKey;
            }
            return null;
        }

        protected ConfigurationElementManageabilityProvider GetSubProvider(Type configurationObjectType)
        {
            if (this.subProviders.ContainsKey(configurationObjectType))
            {
                return this.subProviders[configurationObjectType];
            }
            return null;
        }

        protected static void LoadRegistrySubKeys(string subKeyName, IRegistryKey machineKey, IRegistryKey userKey, out IRegistryKey machineSubKey, out IRegistryKey userSubKey)
        {
            machineSubKey = (machineKey != null) ? machineKey.OpenSubKey(subKeyName) : null;
            userSubKey = (userKey != null) ? userKey.OpenSubKey(subKeyName) : null;
        }

        protected virtual void LogExceptionWhileOverriding(Exception exception)
        {
            ManageabilityExtensionsLogger.LogExceptionWhileOverriding(exception);
        }

        protected internal abstract bool OverrideWithGroupPoliciesAndGenerateWmiObjects(ConfigurationSection configurationObject, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings);
        protected static bool OverrideWithGroupPoliciesAndGenerateWmiObjectsForElement<T>(T element, ConfigurationElementManageabilityProvider subProvider, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings) where T: NamedConfigurationElement, new()
        {
            return subProvider.OverrideWithGroupPoliciesAndGenerateWmiObjects(element, readGroupPolicies, machineKey, userKey, generateWmiObjects, wmiSettings);
        }

        protected void OverrideWithGroupPoliciesAndGenerateWmiObjectsForElementCollection<T>(NamedElementCollection<T> elements, string keyName, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings) where T: NamedConfigurationElement, new()
        {
            List<T> list = new List<T>();
            IRegistryKey machineSubKey = null;
            IRegistryKey userSubKey = null;
            try
            {
                LoadRegistrySubKeys(keyName, machineKey, userKey, out machineSubKey, out userSubKey);
                foreach (T local in elements)
                {
                    IRegistryKey key3 = null;
                    IRegistryKey key4 = null;
                    try
                    {
                        LoadRegistrySubKeys(local.Name, machineSubKey, userSubKey, out key3, out key4);
                        ConfigurationElementManageabilityProvider subProvider = this.GetSubProvider(local.GetType());
                        if ((subProvider != null) && !OverrideWithGroupPoliciesAndGenerateWmiObjectsForElement<T>(local, subProvider, readGroupPolicies, key3, key4, generateWmiObjects, wmiSettings))
                        {
                            list.Add(local);
                        }
                        continue;
                    }
                    finally
                    {
                        ReleaseRegistryKeys(new IRegistryKey[] { key3, key4 });
                    }
                }
            }
            finally
            {
                ReleaseRegistryKeys(new IRegistryKey[] { machineSubKey, userSubKey });
            }
            foreach (T local2 in list)
            {
                elements.Remove(local2.Name);
            }
        }

        protected static void ReleaseRegistryKeys(params IRegistryKey[] keys)
        {
            foreach (IRegistryKey key in keys)
            {
                if (key != null)
                {
                    try
                    {
                        key.Close();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}

