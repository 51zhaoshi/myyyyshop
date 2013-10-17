namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;

    public abstract class ConfigurationSectionManageabilityProviderBase<T> : ConfigurationSectionManageabilityProvider where T: ConfigurationSection
    {
        protected ConfigurationSectionManageabilityProviderBase(IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders) : base(subProviders)
        {
        }

        protected internal sealed override void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, ConfigurationSection configurationObject, IConfigurationSource configurationSource, string applicationName)
        {
            T configurationSection = (T) configurationObject;
            string sectionKey = applicationName + @"\" + this.SectionName;
            contentBuilder.StartCategory(this.SectionCategoryName);
            this.AddAdministrativeTemplateDirectives(contentBuilder, configurationSection, configurationSource, sectionKey);
            contentBuilder.EndCategory();
        }

        protected abstract void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, T configurationSection, IConfigurationSource configurationSource, string sectionKey);
        protected abstract void GenerateWmiObjectsForConfigurationSection(T configurationSection, ICollection<ConfigurationSetting> wmiSettings);
        protected internal sealed override bool OverrideWithGroupPoliciesAndGenerateWmiObjects(ConfigurationSection configurationObject, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            T configurationSection = configurationObject as T;
            if (configurationSection == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Resources.ConfigurationElementOfWrongType, new object[] { typeof(T).FullName, configurationObject.GetType().FullName }), "configurationObject");
            }
            if (!this.OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationSection(configurationSection, readGroupPolicies, machineKey, userKey, generateWmiObjects, wmiSettings))
            {
                return false;
            }
            this.OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationElements(configurationSection, readGroupPolicies, machineKey, userKey, generateWmiObjects, wmiSettings);
            return true;
        }

        protected abstract void OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationElements(T configurationSection, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings);
        private bool OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationSection(T configurationSection, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            if (readGroupPolicies)
            {
                IRegistryKey policyKey = ConfigurationSectionManageabilityProvider.GetPolicyKey(machineKey, userKey);
                if (policyKey != null)
                {
                    if (!policyKey.GetBoolValue("Available").Value)
                    {
                        return false;
                    }
                    try
                    {
                        this.OverrideWithGroupPoliciesForConfigurationSection(configurationSection, policyKey);
                    }
                    catch (Exception exception)
                    {
                        this.LogExceptionWhileOverriding(exception);
                    }
                }
            }
            if (generateWmiObjects)
            {
                this.GenerateWmiObjectsForConfigurationSection(configurationSection, wmiSettings);
            }
            return true;
        }

        protected abstract void OverrideWithGroupPoliciesForConfigurationSection(T configurationSection, IRegistryKey policyKey);

        protected abstract string SectionCategoryName { get; }

        protected abstract string SectionName { get; }
    }
}

