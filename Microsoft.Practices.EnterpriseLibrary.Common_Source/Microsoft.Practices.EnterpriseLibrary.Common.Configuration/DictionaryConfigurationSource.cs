namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;

    public class DictionaryConfigurationSource : IConfigurationSource
    {
        protected internal EventHandlerList eventHandlers = new EventHandlerList();
        protected internal Dictionary<string, ConfigurationSection> sections = new Dictionary<string, ConfigurationSection>();

        public void Add(string name, ConfigurationSection section)
        {
            this.sections.Add(name, section);
        }

        public void Add(IConfigurationParameter saveParameter, string sectionName, ConfigurationSection configurationSection)
        {
            this.Add(sectionName, configurationSection);
        }

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            this.eventHandlers.AddHandler(sectionName, handler);
        }

        public bool Contains(string name)
        {
            return this.sections.ContainsKey(name);
        }

        public ConfigurationSection GetSection(string sectionName)
        {
            if (this.sections.ContainsKey(sectionName))
            {
                return this.sections[sectionName];
            }
            return null;
        }

        public bool Remove(string name)
        {
            return this.sections.Remove(name);
        }

        public void Remove(IConfigurationParameter removeParameter, string sectionName)
        {
            this.Remove(sectionName);
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
            this.eventHandlers.RemoveHandler(sectionName, handler);
        }
    }
}

