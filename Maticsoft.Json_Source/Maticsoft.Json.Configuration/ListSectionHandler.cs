namespace Maticsoft.Json.Configuration
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Xml;

    public abstract class ListSectionHandler : IConfigurationSectionHandler
    {
        private readonly string _elementName;

        protected ListSectionHandler(string elementName)
        {
            if (elementName == null)
            {
                throw new ArgumentNullException("elementName");
            }
            if (elementName.Length == 0)
            {
                throw new ArgumentException(null, "elementName");
            }
            this._elementName = elementName;
        }

        public virtual object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("section");
            }
            IList list = this.CreateList(parent);
            string elementName = this.ElementName;
            foreach (XmlNode node in section.ChildNodes)
            {
                if ((node.NodeType != XmlNodeType.Comment) && (node.NodeType != XmlNodeType.Whitespace))
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        throw new ConfigurationException(string.Format("Unexpected type of node ({0}) in configuration.", node.NodeType.ToString()), node);
                    }
                    if (node.Name != elementName)
                    {
                        throw new ConfigurationException(string.Format("Element <{0}> is not valid here in configuration. Use <{1}> elements only.", node.Name, elementName), node);
                    }
                    list.Add(this.GetItem((XmlElement) node));
                }
            }
            return list;
        }

        protected virtual IList CreateList(object parent)
        {
            if (parent == null)
            {
                return new ArrayList(4);
            }
            return new ArrayList((ICollection) parent);
        }

        protected abstract object GetItem(XmlElement element);

        protected string ElementName
        {
            get
            {
                return this._elementName;
            }
        }
    }
}

