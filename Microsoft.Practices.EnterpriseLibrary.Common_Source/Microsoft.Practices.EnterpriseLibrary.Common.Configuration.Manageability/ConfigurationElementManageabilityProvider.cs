namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public abstract class ConfigurationElementManageabilityProvider
    {
        public const string PolicyValueName = "Available";

        protected ConfigurationElementManageabilityProvider()
        {
        }

        protected internal abstract void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, ConfigurationElement configurationObject, IConfigurationSource configurationSource, string parentKey);
        protected virtual void LogExceptionWhileOverriding(Exception exception)
        {
            ManageabilityExtensionsLogger.LogExceptionWhileOverriding(exception);
        }

        protected internal abstract bool OverrideWithGroupPoliciesAndGenerateWmiObjects(ConfigurationElement configurationObject, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings);
    }
}

