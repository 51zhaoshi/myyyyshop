namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class ManageabilityHelper : IManageabilityHelper
    {
        private string applicationName;
        private bool generateWmiObjects;
        private IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders;
        private IDictionary<string, IEnumerable<ConfigurationSetting>> publishedSettingsMapping;
        private bool readGroupPolicies;
        private IRegistryAccessor registryAccessor;
        private IWmiPublisher wmiPublisher;

        public ManageabilityHelper(IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders, bool readGroupPolicies, bool generateWmiObjects, string applicationName) : this(manageabilityProviders, readGroupPolicies, new RegistryAccessor(), generateWmiObjects, new InstrumentationWmiPublisher(), applicationName)
        {
        }

        public ManageabilityHelper(IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders, bool readGroupPolicies, IRegistryAccessor registryAccessor, bool generateWmiObjects, IWmiPublisher wmiPublisher, string applicationName)
        {
            this.manageabilityProviders = manageabilityProviders;
            this.readGroupPolicies = readGroupPolicies;
            this.registryAccessor = registryAccessor;
            this.generateWmiObjects = generateWmiObjects;
            this.wmiPublisher = wmiPublisher;
            this.applicationName = applicationName;
            this.publishedSettingsMapping = new Dictionary<string, IEnumerable<ConfigurationSetting>>();
        }

        internal static string BuildSectionKeyName(string applicationName, string sectionName)
        {
            return Path.Combine(Path.Combine(@"Software\Policies\", applicationName), sectionName);
        }

        private void LoadPolicyRegistryKeys(string sectionName, out IRegistryKey machineKey, out IRegistryKey userKey)
        {
            if (this.readGroupPolicies)
            {
                string name = BuildSectionKeyName(this.applicationName, sectionName);
                machineKey = this.registryAccessor.LocalMachine.OpenSubKey(name);
                userKey = this.registryAccessor.CurrentUser.OpenSubKey(name);
            }
            else
            {
                machineKey = null;
                userKey = null;
            }
        }

        private void PublishAll(IEnumerable<ConfigurationSetting> instances, string sectionName)
        {
            foreach (ConfigurationSetting setting in instances)
            {
                setting.ApplicationName = this.applicationName;
                setting.SectionName = sectionName;
                this.wmiPublisher.Publish(setting);
            }
        }

        private static void ReleasePolicyRegistryKeys(IRegistryKey machineKey, IRegistryKey userKey)
        {
            if (machineKey != null)
            {
                try
                {
                    machineKey.Close();
                }
                catch (Exception)
                {
                }
            }
            if (userKey != null)
            {
                try
                {
                    userKey.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        public void RevokeAll(IEnumerable<ConfigurationSetting> instances)
        {
            foreach (ConfigurationSetting setting in instances)
            {
                this.wmiPublisher.Revoke(setting);
            }
        }

        public void UpdateConfigurationManageability(IConfigurationAccessor configurationAccessor)
        {
            using (new GroupPolicyLock())
            {
                foreach (string str in this.manageabilityProviders.Keys)
                {
                    if (this.publishedSettingsMapping.ContainsKey(str))
                    {
                        this.RevokeAll(this.publishedSettingsMapping[str]);
                    }
                    ConfigurationSectionManageabilityProvider provider = this.manageabilityProviders[str];
                    ConfigurationSection configurationObject = configurationAccessor.GetSection(str);
                    if (configurationObject != null)
                    {
                        ICollection<ConfigurationSetting> wmiSettings = new List<ConfigurationSetting>();
                        IRegistryKey machineKey = null;
                        IRegistryKey userKey = null;
                        try
                        {
                            try
                            {
                                this.LoadPolicyRegistryKeys(str, out machineKey, out userKey);
                                if (provider.OverrideWithGroupPoliciesAndGenerateWmiObjects(configurationObject, this.readGroupPolicies, machineKey, userKey, this.generateWmiObjects, wmiSettings))
                                {
                                    this.publishedSettingsMapping[str] = wmiSettings;
                                    this.PublishAll(wmiSettings, str);
                                }
                                else
                                {
                                    configurationAccessor.RemoveSection(str);
                                }
                            }
                            catch (Exception exception)
                            {
                                ManageabilityExtensionsLogger.LogException(exception, Resources.ExceptionUnexpectedErrorProcessingSection);
                            }
                            continue;
                        }
                        finally
                        {
                            ReleasePolicyRegistryKeys(machineKey, userKey);
                        }
                    }
                }
            }
        }

        internal IDictionary<string, ConfigurationSectionManageabilityProvider> ManageabilityProviders
        {
            get
            {
                return this.manageabilityProviders;
            }
        }

        private class GroupPolicyLock : IDisposable
        {
            private IntPtr machineCriticalSectionHandle;
            private IntPtr userCriticalSectionHandle = Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.EnterCriticalPolicySection(false);

            public GroupPolicyLock()
            {
                if (IntPtr.Zero == this.userCriticalSectionHandle)
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }
                this.machineCriticalSectionHandle = Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.EnterCriticalPolicySection(true);
                if (IntPtr.Zero == this.machineCriticalSectionHandle)
                {
                    int errorCode = Marshal.GetHRForLastWin32Error();
                    Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.LeaveCriticalPolicySection(this.userCriticalSectionHandle);
                    Marshal.ThrowExceptionForHR(errorCode);
                }
            }

            void IDisposable.Dispose()
            {
                Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.LeaveCriticalPolicySection(this.machineCriticalSectionHandle);
                Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.LeaveCriticalPolicySection(this.userCriticalSectionHandle);
            }
        }
    }
}

