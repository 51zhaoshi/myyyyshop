namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Runtime.InteropServices;

    public class FileConfigurationSourceImplementation : BaseFileConfigurationSourceImplementation
    {
        private System.Configuration.Configuration cachedConfiguration;
        private object cachedConfigurationLock;
        private string configurationFilepath;
        private ExeConfigurationFileMap fileMap;

        public FileConfigurationSourceImplementation(string configurationFilepath) : this(configurationFilepath, true)
        {
        }

        public FileConfigurationSourceImplementation(string configurationFilepath, bool refresh) : base(configurationFilepath, refresh)
        {
            this.cachedConfigurationLock = new object();
            this.configurationFilepath = configurationFilepath;
            this.fileMap = new ExeConfigurationFileMap();
            this.fileMap.ExeConfigFilename = configurationFilepath;
        }

        private System.Configuration.Configuration GetConfiguration()
        {
            if (this.cachedConfiguration == null)
            {
                lock (this.cachedConfigurationLock)
                {
                    if (this.cachedConfiguration == null)
                    {
                        this.cachedConfiguration = ConfigurationManager.OpenMappedExeConfiguration(this.fileMap, ConfigurationUserLevel.None);
                    }
                }
            }
            return this.cachedConfiguration;
        }

        public override ConfigurationSection GetSection(string sectionName)
        {
            ConfigurationSection configurationSection = this.GetConfiguration().GetSection(sectionName);
            base.SetConfigurationWatchers(sectionName, configurationSection);
            return configurationSection;
        }

        protected override void RefreshAndValidateSections(IDictionary<string, string> localSectionsToRefresh, IDictionary<string, string> externalSectionsToRefresh, out ICollection<string> sectionsToNotify, out IDictionary<string, string> sectionsWithChangedConfigSource)
        {
            this.UpdateCache();
            sectionsToNotify = new List<string>();
            sectionsWithChangedConfigSource = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in localSectionsToRefresh)
            {
                ConfigurationSection section = this.cachedConfiguration.GetSection(pair.Key);
                string str = (section != null) ? section.SectionInformation.ConfigSource : "__null__";
                if (!pair.Value.Equals(str))
                {
                    sectionsWithChangedConfigSource.Add(pair.Key, str);
                }
                sectionsToNotify.Add(pair.Key);
            }
            foreach (KeyValuePair<string, string> pair2 in externalSectionsToRefresh)
            {
                ConfigurationSection section2 = this.cachedConfiguration.GetSection(pair2.Key);
                string str2 = (section2 != null) ? section2.SectionInformation.ConfigSource : "__null__";
                if (!pair2.Value.Equals(str2))
                {
                    sectionsWithChangedConfigSource.Add(pair2.Key, str2);
                    sectionsToNotify.Add(pair2.Key);
                }
            }
        }

        protected override void RefreshExternalSections(string[] sectionsToRefresh)
        {
            this.UpdateCache();
        }

        internal void UpdateCache()
        {
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(this.fileMap, ConfigurationUserLevel.None);
            lock (this.cachedConfigurationLock)
            {
                this.cachedConfiguration = configuration;
            }
        }
    }
}

