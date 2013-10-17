namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    internal class ManageableConfigurationSourceImplementation : IDisposable
    {
        private object configurationUpdateLock;
        private IConfigurationAccessor currentConfigurationAccessor;
        private ExeConfigurationFileMap fileMap;
        private IGroupPolicyWatcher groupPolicyWatcher;
        private IManageabilityHelper manageabilityHelper;
        private ConfigurationChangeNotificationCoordinator notificationCoordinator;
        private ConfigurationChangeWatcherCoordinator watcherCoordinator;

        internal ManageableConfigurationSourceImplementation(string configurationFilePath, IManageabilityHelper manageabilityHelper, IGroupPolicyWatcher groupPolicyWatcher, ConfigurationChangeWatcherCoordinator watcherCoordinator, ConfigurationChangeNotificationCoordinator notificationCoordinator)
        {
            this.configurationUpdateLock = new object();
            this.fileMap = new ExeConfigurationFileMap();
            this.fileMap.ExeConfigFilename = configurationFilePath;
            this.manageabilityHelper = manageabilityHelper;
            this.notificationCoordinator = notificationCoordinator;
            this.AttachGroupPolicyWatcher(groupPolicyWatcher);
            this.AttachWatcherCoordinator(watcherCoordinator);
            this.InitializeConfiguration();
        }

        public ManageableConfigurationSourceImplementation(string configurationFilePath, bool refresh, IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders, bool readGroupPolicies, bool generateWmiObjects, string applicationName) : this(configurationFilePath, new Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.ManageabilityHelper(manageabilityProviders, readGroupPolicies, generateWmiObjects, applicationName), new GroupPolicyWatcher(), new ConfigurationChangeWatcherCoordinator(configurationFilePath, refresh), new ConfigurationChangeNotificationCoordinator())
        {
        }

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            this.notificationCoordinator.AddSectionChangeHandler(sectionName, handler);
        }

        private void AttachGroupPolicyWatcher(IGroupPolicyWatcher groupPolicyWatcher)
        {
            this.groupPolicyWatcher = groupPolicyWatcher;
            this.groupPolicyWatcher.GroupPolicyUpdated += new GroupPolicyUpdateDelegate(this.OnGroupPolicyUpdated);
            this.groupPolicyWatcher.StartWatching();
        }

        private void AttachWatcherCoordinator(ConfigurationChangeWatcherCoordinator watcherCoordinator)
        {
            this.watcherCoordinator = watcherCoordinator;
            this.watcherCoordinator.ConfigurationChanged += new ConfigurationChangedEventHandler(this.OnConfigurationChanged);
        }

        public void Dispose()
        {
            this.groupPolicyWatcher.Dispose();
            this.watcherCoordinator.Dispose();
        }

        public ConfigurationSection GetSection(string sectionName)
        {
            ConfigurationSection section = this.currentConfigurationAccessor.GetSection(sectionName);
            if (section != null)
            {
                lock (this.configurationUpdateLock)
                {
                    this.watcherCoordinator.SetWatcherForConfigSource(section.SectionInformation.ConfigSource);
                }
            }
            return section;
        }

        private void InitializeConfiguration()
        {
            this.currentConfigurationAccessor = new ConfigurationInstanceConfigurationAccessor(ConfigurationManager.OpenMappedExeConfiguration(this.fileMap, ConfigurationUserLevel.None));
            this.manageabilityHelper.UpdateConfigurationManageability(this.currentConfigurationAccessor);
            foreach (string str in this.currentConfigurationAccessor.GetRequestedSectionNames())
            {
                ConfigurationSection section = this.currentConfigurationAccessor.GetSection(str);
                if (section != null)
                {
                    this.watcherCoordinator.SetWatcherForConfigSource(section.SectionInformation.ConfigSource);
                }
            }
        }

        private void OnConfigurationChanged(object sender, ConfigurationChangedEventArgs e)
        {
            this.UpdateConfiguration(e.SectionName);
        }

        private void OnGroupPolicyUpdated(bool machine)
        {
            this.UpdateConfiguration("");
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            this.notificationCoordinator.RemoveSectionChangeHandler(sectionName, handler);
        }

        private void UpdateConfiguration(string configSource)
        {
            lock (this.configurationUpdateLock)
            {
                ConfigurationInstanceConfigurationAccessor configurationAccessor = new ConfigurationInstanceConfigurationAccessor(ConfigurationManager.OpenMappedExeConfiguration(this.fileMap, ConfigurationUserLevel.None));
                this.manageabilityHelper.UpdateConfigurationManageability(configurationAccessor);
                List<string> sectionsToNotify = new List<string>();
                bool flag = "".Equals(configSource);
                foreach (string str in this.currentConfigurationAccessor.GetRequestedSectionNames())
                {
                    ConfigurationSection currentSection = this.currentConfigurationAccessor.GetSection(str);
                    ConfigurationSection section = configurationAccessor.GetSection(str);
                    if ((currentSection != null) || (section != null))
                    {
                        this.UpdateWatchers(currentSection, section);
                        if ((flag || (section == null)) || ((configSource.Equals(section.SectionInformation.ConfigSource) || (currentSection == null)) || configSource.Equals(currentSection.SectionInformation.ConfigSource)))
                        {
                            sectionsToNotify.Add(str);
                        }
                    }
                }
                this.currentConfigurationAccessor = configurationAccessor;
                this.notificationCoordinator.NotifyUpdatedSections(sectionsToNotify);
            }
        }

        private void UpdateWatchers(ConfigurationSection currentSection, ConfigurationSection updatedSection)
        {
            if (currentSection != null)
            {
                if (((updatedSection == null) || !currentSection.SectionInformation.ConfigSource.Equals(updatedSection.SectionInformation.ConfigSource)) && !"".Equals(currentSection.SectionInformation.ConfigSource))
                {
                    this.watcherCoordinator.RemoveWatcherForConfigSource(currentSection.SectionInformation.ConfigSource);
                }
                if (updatedSection != null)
                {
                    this.watcherCoordinator.SetWatcherForConfigSource(updatedSection.SectionInformation.ConfigSource);
                }
            }
            else
            {
                this.watcherCoordinator.SetWatcherForConfigSource(updatedSection.SectionInformation.ConfigSource);
            }
        }

        internal IManageabilityHelper ManageabilityHelper
        {
            get
            {
                return this.manageabilityHelper;
            }
        }
    }
}

