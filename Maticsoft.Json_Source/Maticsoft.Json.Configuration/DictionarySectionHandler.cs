namespace Maticsoft.Json.Configuration
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Xml;

    public class DictionarySectionHandler : IConfigurationSectionHandler
    {
        public virtual object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("section");
            }
            IDictionary dictionary = this.CreateDictionary(parent);
            string keyName = this.KeyName;
            foreach (XmlNode node in section.ChildNodes)
            {
                if ((node.NodeType == XmlNodeType.Comment) || (node.NodeType == XmlNodeType.Whitespace))
                {
                    continue;
                }
                if (node.NodeType != XmlNodeType.Element)
                {
                    throw new ConfigurationException(string.Format("Unexpected type of node ({0}) in configuration.", node.NodeType.ToString()), node);
                }
                string name = node.Name;
                if (name == "clear")
                {
                    this.OnClear(dictionary);
                    continue;
                }
                XmlAttribute attribute = node.Attributes[keyName];
                string key = (attribute == null) ? null : attribute.Value;
                if ((key == null) || (key.Length == 0))
                {
                    throw new ConfigurationException("Missing entry key.", node);
                }
                if (name != "add")
                {
                    if (name != "remove")
                    {
                        throw new ConfigurationException(string.Format("'{0}' is not a valid dictionary node. Use add, remove or clear.", name), node);
                    }
                    this.OnRemove(dictionary, key);
                }
                else
                {
                    this.OnAdd(dictionary, key, node);
                    continue;
                }
            }
            return dictionary;
        }

        protected virtual IDictionary CreateDictionary(object parent)
        {
            CaseInsensitiveHashCodeProvider defaultInvariant = CaseInsensitiveHashCodeProvider.DefaultInvariant;
            CaseInsensitiveComparer comparer = CaseInsensitiveComparer.DefaultInvariant;
            if (parent == null)
            {
                return new Hashtable(defaultInvariant, comparer);
            }
            return new Hashtable((IDictionary) parent, defaultInvariant, comparer);
        }

        protected virtual void OnAdd(IDictionary dictionary, string key, XmlNode node)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            XmlAttribute attribute = node.Attributes[this.ValueName];
            dictionary.Add(key, (attribute != null) ? attribute.Value : null);
        }

        protected virtual void OnClear(IDictionary dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            dictionary.Clear();
        }

        protected virtual void OnRemove(IDictionary dictionary, string key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            dictionary.Remove(key);
        }

        protected virtual string KeyName
        {
            get
            {
                return "key";
            }
        }

        protected virtual string ValueName
        {
            get
            {
                return "value";
            }
        }
    }
}

