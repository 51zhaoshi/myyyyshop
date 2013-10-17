namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;

    public abstract class ConfigurationElementManageabilityProviderBase<T> : ConfigurationElementManageabilityProvider where T: NamedConfigurationElement
    {
        protected ConfigurationElementManageabilityProviderBase()
        {
        }

        protected virtual void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, T configurationObject, IConfigurationSource configurationSource, string elementPolicyKeyName)
        {
            contentBuilder.StartPolicy(string.Format(CultureInfo.InvariantCulture, this.ElementPolicyNameTemplate, new object[] { configurationObject.Name }), elementPolicyKeyName);
            this.AddElementAdministrativeTemplateParts(contentBuilder, configurationObject, configurationSource, elementPolicyKeyName);
            contentBuilder.EndPolicy();
        }

        protected internal sealed override void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, ConfigurationElement configurationObject, IConfigurationSource configurationSource, string parentKey)
        {
            T local = (T) configurationObject;
            string elementPolicyKeyName = parentKey + @"\" + local.Name;
            this.AddAdministrativeTemplateDirectives(contentBuilder, local, configurationSource, elementPolicyKeyName);
        }

        protected abstract void AddElementAdministrativeTemplateParts(AdmContentBuilder contentBuilder, T configurationObject, IConfigurationSource configurationSource, string elementPolicyKeyName);
        protected abstract void GenerateWmiObjects(T configurationObject, ICollection<ConfigurationSetting> wmiSettings);
        protected abstract void OverrideWithGroupPolicies(T configurationObject, IRegistryKey policyKey);
        protected internal sealed override bool OverrideWithGroupPoliciesAndGenerateWmiObjects(ConfigurationElement configurationObject, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            T local = configurationObject as T;
            if (local == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Resources.ConfigurationElementOfWrongType, new object[] { typeof(T).FullName, configurationObject.GetType().FullName }), "configurationObject");
            }
            if (readGroupPolicies)
            {
                IRegistryKey policyKey = (machineKey != null) ? machineKey : userKey;
                if (policyKey != null)
                {
                    if (policyKey.IsPolicyKey && !policyKey.GetBoolValue("Available").Value)
                    {
                        return false;
                    }
                    try
                    {
                        this.OverrideWithGroupPolicies(local, policyKey);
                    }
                    catch (Exception exception)
                    {
                        this.LogExceptionWhileOverriding(exception);
                    }
                }
            }
            if (generateWmiObjects)
            {
                this.GenerateWmiObjects(local, wmiSettings);
            }
            return true;
        }

        protected abstract string ElementPolicyNameTemplate { get; }
    }
}

