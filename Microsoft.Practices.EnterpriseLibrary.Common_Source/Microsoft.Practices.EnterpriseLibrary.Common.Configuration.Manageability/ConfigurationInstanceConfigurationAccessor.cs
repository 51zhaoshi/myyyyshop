namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    internal class ConfigurationInstanceConfigurationAccessor : IConfigurationAccessor
    {
        private System.Configuration.Configuration configuration;
        private IDictionary<string, bool> requestedSections;

        public ConfigurationInstanceConfigurationAccessor(System.Configuration.Configuration configuration)
        {
            this.configuration = configuration;
            this.requestedSections = new Dictionary<string, bool>();
        }

        public IEnumerable<string> GetRequestedSectionNames()
        {
            string[] array = new string[this.requestedSections.Keys.Count];
            this.requestedSections.Keys.CopyTo(array, 0);
            return array;
        }

        public ConfigurationSection GetSection(string sectionName)
        {
            ConfigurationSection section = this.configuration.GetSection(sectionName);
            this.requestedSections[sectionName] = section != null;
            return section;
        }

        public void RemoveSection(string sectionName)
        {
            this.configuration.Sections.Remove(sectionName);
        }
    }
}

