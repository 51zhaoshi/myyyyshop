namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Xml;

    public abstract class PolymorphicConfigurationElementCollection<T> : NamedElementCollection<T> where T: NamedConfigurationElement, new()
    {
        private Dictionary<string, Type> configurationElementTypeMapping;
        private T currentElement;

        protected PolymorphicConfigurationElementCollection()
        {
        }

        protected override ConfigurationElement CreateNewElement()
        {
            if (this.currentElement != null)
            {
                return this.currentElement;
            }
            return Activator.CreateInstance<T>();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            if (this.configurationElementTypeMapping != null)
            {
                Type type = this.configurationElementTypeMapping[elementName];
                if (type != null)
                {
                    return (Activator.CreateInstance(type) as ConfigurationElement);
                }
            }
            return base.CreateNewElement(elementName);
        }

        private void CreateTypesMap(PolymorphicConfigurationElementCollection<T> sourceCollection)
        {
            this.configurationElementTypeMapping = new Dictionary<string, Type>(sourceCollection.Count);
            foreach (T local in sourceCollection)
            {
                this.configurationElementTypeMapping.Add(local.Name, local.GetType());
            }
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
        {
            if (base.AddElementName.Equals(elementName))
            {
                Type type = this.RetrieveConfigurationElementType(reader);
                this.currentElement = (T) Activator.CreateInstance(type);
                this.currentElement.DeserializeElement(reader);
                base.Add(this.currentElement);
                return true;
            }
            return base.OnDeserializeUnrecognizedElement(elementName, reader);
        }

        private void ReleaseTypesMap()
        {
            this.configurationElementTypeMapping = null;
        }

        protected abstract Type RetrieveConfigurationElementType(XmlReader reader);
        protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
        {
            this.CreateTypesMap((PolymorphicConfigurationElementCollection<T>) sourceElement);
            base.Unmerge(sourceElement, parentElement, saveMode);
            this.ReleaseTypesMap();
        }
    }
}

