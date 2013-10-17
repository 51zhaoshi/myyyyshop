namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    [ConfigurationElementType(typeof(ManageableConfigurationSourceElement))]
    public class ManageableConfigurationSource : IConfigurationSource
    {
        private ManageableConfigurationSourceImplementation implementation;
        private static ManageableConfigurationSourceSingletonHelper singletonHelper = new ManageableConfigurationSourceSingletonHelper();

        internal ManageableConfigurationSource(ManageableConfigurationSourceImplementation implementation)
        {
            this.implementation = implementation;
        }

        public ManageableConfigurationSource(string configurationFilePath, IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders, bool readGroupPolicies, bool generateWmiObjects, string applicationName) : this(GetManageableConfigurationSourceImplementation(configurationFilePath, manageabilityProviders, readGroupPolicies, generateWmiObjects, applicationName))
        {
        }

        public void Add(IConfigurationParameter saveParameter, string sectionName, ConfigurationSection configurationSection)
        {
            throw new NotImplementedException(Resources.ManageableConfigurationSourceUpdateNotAvailable);
        }

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            this.CheckSectionName(sectionName);
            this.CheckHandler(handler);
            this.implementation.AddSectionChangeHandler(sectionName, handler);
        }

        private static void CheckApplicationName(string applicationName)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new ArgumentNullException("applicationName");
            }
            if (applicationName.Length > 0xff)
            {
                throw new ArgumentException(Resources.ExceptionApplicationNameTooLong, "applicationName");
            }
        }

        private static void CheckFilePath(string configurationFilePath)
        {
            if (string.IsNullOrEmpty(configurationFilePath))
            {
                throw new ArgumentNullException("configurationFilePath");
            }
        }

        private void CheckHandler(ConfigurationChangedEventHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
        }

        private static void CheckProvidersMapping(IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders)
        {
            if (manageabilityProviders == null)
            {
                throw new ArgumentNullException("manageabilityProviders");
            }
        }

        private void CheckSectionName(string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                throw new ArgumentNullException("sectionName");
            }
        }

        private static ManageableConfigurationSourceImplementation GetManageableConfigurationSourceImplementation(string configurationFilePath, IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders, bool readGroupPolicies, bool generateWmiObjects, string applicationName)
        {
            CheckFilePath(configurationFilePath);
            CheckApplicationName(applicationName);
            CheckProvidersMapping(manageabilityProviders);
            return singletonHelper.GetInstance(configurationFilePath, manageabilityProviders, readGroupPolicies, generateWmiObjects, applicationName);
        }

        public ConfigurationSection GetSection(string sectionName)
        {
            this.CheckSectionName(sectionName);
            return this.implementation.GetSection(sectionName);
        }

        public void Remove(IConfigurationParameter removeParameter, string sectionName)
        {
            throw new NotImplementedException(Resources.ManageableConfigurationSourceUpdateNotAvailable);
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            this.CheckSectionName(sectionName);
            this.CheckHandler(handler);
            this.implementation.RemoveSectionChangeHandler(sectionName, handler);
        }

        internal static ManageableConfigurationSourceSingletonHelper ResetSingletonHelper()
        {
            ManageableConfigurationSourceSingletonHelper singletonHelper = ManageableConfigurationSource.singletonHelper;
            ManageableConfigurationSource.singletonHelper = new ManageableConfigurationSourceSingletonHelper();
            singletonHelper.Dispose();
            return ManageableConfigurationSource.singletonHelper;
        }

        internal ManageableConfigurationSourceImplementation Implementation
        {
            get
            {
                return this.implementation;
            }
        }
    }
}

