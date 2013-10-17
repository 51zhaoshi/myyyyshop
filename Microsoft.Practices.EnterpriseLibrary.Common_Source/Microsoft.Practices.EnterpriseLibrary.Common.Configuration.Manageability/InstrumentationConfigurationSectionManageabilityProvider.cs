namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration;
    using System;
    using System.Collections.Generic;

    internal class InstrumentationConfigurationSectionManageabilityProvider : ConfigurationSectionManageabilityProviderBase<InstrumentationConfigurationSection>
    {
        public const string EventLoggingEnabledPropertyName = "eventLoggingEnabled";
        public const string PerformanceCountersEnabledPropertyName = "performanceCountersEnabled";
        public const string WmiEnabledPropertyName = "wmiEnabled";

        public InstrumentationConfigurationSectionManageabilityProvider(IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders) : base(subProviders)
        {
        }

        protected override void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder, InstrumentationConfigurationSection configurationSection, IConfigurationSource configurationSource, string sectionKey)
        {
            contentBuilder.StartPolicy(Resources.InstrumentationSectionPolicyName, sectionKey);
            contentBuilder.AddCheckboxPart(Resources.InstrumentationSectionEventLoggingEnabledPartName, "eventLoggingEnabled", configurationSection.EventLoggingEnabled);
            contentBuilder.AddCheckboxPart(Resources.InstrumentationSectionPerformanceCountersEnabledPartName, "performanceCountersEnabled", configurationSection.PerformanceCountersEnabled);
            contentBuilder.AddCheckboxPart(Resources.InstrumentationSectionWmiEnabledPartName, "wmiEnabled", configurationSection.WmiEnabled);
            contentBuilder.EndPolicy();
        }

        protected override void GenerateWmiObjectsForConfigurationSection(InstrumentationConfigurationSection configurationSection, ICollection<ConfigurationSetting> wmiSettings)
        {
            wmiSettings.Add(new InstrumentationSetting(configurationSection.EventLoggingEnabled, configurationSection.PerformanceCountersEnabled, configurationSection.WmiEnabled));
        }

        protected override void OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationElements(InstrumentationConfigurationSection configurationSection, bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey, bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
        }

        protected override void OverrideWithGroupPoliciesForConfigurationSection(InstrumentationConfigurationSection configurationSection, IRegistryKey policyKey)
        {
            bool? boolValue = policyKey.GetBoolValue("eventLoggingEnabled");
            bool? nullable2 = policyKey.GetBoolValue("performanceCountersEnabled");
            bool? nullable3 = policyKey.GetBoolValue("wmiEnabled");
            configurationSection.EventLoggingEnabled = boolValue.Value;
            configurationSection.PerformanceCountersEnabled = nullable2.Value;
            configurationSection.WmiEnabled = nullable3.Value;
        }

        protected override string SectionCategoryName
        {
            get
            {
                return Resources.InstrumentationSectionCategoryName;
            }
        }

        protected override string SectionName
        {
            get
            {
                return "instrumentationConfiguration";
            }
        }
    }
}

