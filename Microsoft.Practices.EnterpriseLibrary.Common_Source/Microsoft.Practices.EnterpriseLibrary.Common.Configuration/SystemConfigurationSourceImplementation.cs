namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Runtime.InteropServices;

    public class SystemConfigurationSourceImplementation : BaseFileConfigurationSourceImplementation
    {
        public SystemConfigurationSourceImplementation() : base(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
        {
        }

        public SystemConfigurationSourceImplementation(bool refresh) : base(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, refresh)
        {
        }

        public override ConfigurationSection GetSection(string sectionName)
        {
            ConfigurationSection configurationSection = ConfigurationManager.GetSection(sectionName) as ConfigurationSection;
            base.SetConfigurationWatchers(sectionName, configurationSection);
            return configurationSection;
        }

        protected override void RefreshAndValidateSections(IDictionary<string, string> localSectionsToRefresh, IDictionary<string, string> externalSectionsToRefresh, out ICollection<string> sectionsToNotify, out IDictionary<string, string> sectionsWithChangedConfigSource)
        {
            sectionsToNotify = new List<string>();
            sectionsWithChangedConfigSource = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in localSectionsToRefresh)
            {
                ConfigurationManager.RefreshSection(pair.Key);
                ConfigurationSection section = ConfigurationManager.GetSection(pair.Key) as ConfigurationSection;
                string str = (section != null) ? section.SectionInformation.ConfigSource : "__null__";
                if (!pair.Value.Equals(str))
                {
                    sectionsWithChangedConfigSource.Add(pair.Key, str);
                }
                sectionsToNotify.Add(pair.Key);
            }
            foreach (KeyValuePair<string, string> pair2 in externalSectionsToRefresh)
            {
                ConfigurationManager.RefreshSection(pair2.Key);
                ConfigurationSection section2 = ConfigurationManager.GetSection(pair2.Key) as ConfigurationSection;
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
            foreach (string str in sectionsToRefresh)
            {
                ConfigurationManager.RefreshSection(str);
            }
        }
    }
}

