namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;

    public class CustomProviderDataHelper<T> where T: NameTypeConfigurationElement, IHelperAssistedCustomConfigurationData<T>
    {
        private NameValueCollection attributes;
        private IHelperAssistedCustomConfigurationData<T> helpedCustomProviderData;
        private object lockObject;
        protected internal ConfigurationPropertyCollection propertiesCollection;

        public CustomProviderDataHelper(T helpedCustomProviderData)
        {
            this.lockObject = new object();
            this.propertiesCollection = new ConfigurationPropertyCollection();
            foreach (ConfigurationProperty property in helpedCustomProviderData.Properties)
            {
                this.propertiesCollection.Add(property);
            }
            this.helpedCustomProviderData = helpedCustomProviderData;
        }

        private void AddAttributesFromConfigurationProperties()
        {
            foreach (ConfigurationProperty property in this.propertiesCollection)
            {
                if (!this.IsKnownPropertyName(property.Name))
                {
                    this.attributes.Add(property.Name, (string) this.helpedCustomProviderData.BaseGetPropertyValue(property));
                }
            }
        }

        private bool CopyPropertiesToAttributes()
        {
            bool flag = false;
            foreach (string str in this.attributes)
            {
                string str2 = this.attributes[str];
                string propertyValue = this.GetPropertyValue(str);
                if ((propertyValue == null) || (str2 != propertyValue))
                {
                    this.SetPropertyValue(str, str2);
                    flag = true;
                }
            }
            return flag;
        }

        private void CreateAttributes()
        {
            if (this.attributes == null)
            {
                lock (this.lockObject)
                {
                    if (this.attributes == null)
                    {
                        this.attributes = new NameValueCollection(StringComparer.InvariantCulture);
                        this.AddAttributesFromConfigurationProperties();
                    }
                }
            }
        }

        private ConfigurationProperty CreateProperty(string propertyName)
        {
            ConfigurationProperty property = new ConfigurationProperty(propertyName, typeof(string), null);
            this.propertiesCollection.Add(property);
            return property;
        }

        private void CreateRemoveList(List<string> removeList)
        {
            foreach (ConfigurationProperty property in this.propertiesCollection)
            {
                if (!this.IsKnownPropertyName(property.Name) && (this.attributes.Get(property.Name) == null))
                {
                    removeList.Add(property.Name);
                }
            }
        }

        private ConfigurationProperty GetProperty(string propertyName)
        {
            if (!this.propertiesCollection.Contains(propertyName))
            {
                return null;
            }
            return this.propertiesCollection[propertyName];
        }

        private string GetPropertyValue(string propertyName)
        {
            ConfigurationProperty property = this.GetProperty(propertyName);
            if (property != null)
            {
                return (string) this.helpedCustomProviderData.BaseGetPropertyValue(property);
            }
            return string.Empty;
        }

        public bool HandleIsModified()
        {
            if (!this.UpdatePropertyCollection())
            {
                return this.helpedCustomProviderData.BaseIsModified();
            }
            return true;
        }

        public bool HandleOnDeserializeUnrecognizedAttribute(string name, string value)
        {
            this.Attributes.Add(name, value);
            return true;
        }

        public void HandleReset(ConfigurationElement parentElement)
        {
            T local = parentElement as T;
            if (local != null)
            {
                local.Helper.UpdatePropertyCollection();
            }
            this.helpedCustomProviderData.BaseReset(parentElement);
        }

        public void HandleSetAttributeValue(string key, string value)
        {
            this.Attributes.Add(key, value);
            this.UpdatePropertyCollection();
        }

        public void HandleUnmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
        {
            T local = parentElement as T;
            if (local != null)
            {
                local.Helper.UpdatePropertyCollection();
            }
            T local2 = sourceElement as T;
            if (local2 != null)
            {
                local2.Helper.UpdatePropertyCollection();
            }
            this.helpedCustomProviderData.BaseUnmerge(sourceElement, parentElement, saveMode);
            this.UpdatePropertyCollection();
        }

        protected internal virtual bool IsKnownPropertyName(string propertyName)
        {
            return ((NameTypeConfigurationElement) this.helpedCustomProviderData).Properties.Contains(propertyName);
        }

        private bool RemoveDeletedConfigurationProperties()
        {
            List<string> removeList = new List<string>();
            this.CreateRemoveList(removeList);
            foreach (string str in removeList)
            {
                this.propertiesCollection.Remove(str);
            }
            return (removeList.Count > 0);
        }

        private void SetPropertyValue(string propertyName, string value)
        {
            ConfigurationProperty property = this.GetProperty(propertyName);
            if (property == null)
            {
                property = this.CreateProperty(propertyName);
            }
            this.helpedCustomProviderData.BaseSetPropertyValue(property, value);
        }

        private bool UpdatePropertyCollection()
        {
            if (this.attributes == null)
            {
                return false;
            }
            return (this.RemoveDeletedConfigurationProperties() | this.CopyPropertiesToAttributes());
        }

        public NameValueCollection Attributes
        {
            get
            {
                this.CreateAttributes();
                return this.attributes;
            }
        }

        public ConfigurationPropertyCollection Properties
        {
            get
            {
                this.UpdatePropertyCollection();
                return this.propertiesCollection;
            }
        }
    }
}

