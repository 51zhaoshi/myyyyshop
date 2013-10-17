namespace Maticsoft.Json.Configuration
{
    using Maticsoft.Json;
    using System;
    using System.Configuration;
    using System.Xml;

    public class TypeListSectionHandler : ListSectionHandler
    {
        private readonly Type _expectedType;

        public TypeListSectionHandler(string elementName) : this(elementName, null)
        {
        }

        public TypeListSectionHandler(string elementName, Type expectedType) : base(elementName)
        {
            this._expectedType = expectedType;
        }

        protected override object GetItem(XmlElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            string attribute = element.GetAttribute("type");
            if (attribute.Length == 0)
            {
                throw new ConfigurationException(string.Format("Missing type name specification on <{0}> element.", base.ElementName), element);
            }
            Type type = this.GetType(attribute);
            this.ValidateType(type, element);
            return type;
        }

        protected virtual Type GetType(string typeName)
        {
            if (typeName == null)
            {
                throw new ArgumentNullException("typeName");
            }
            return TypeResolution.GetType(typeName);
        }

        protected virtual void ValidateType(Type type, XmlElement element)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if ((this.ExpectedType != null) && !this.ExpectedType.IsAssignableFrom(type))
            {
                throw new ConfigurationException(string.Format("The type {0} is not valid for the <{2}> configuration element. It must be compatible with the type {1}.", type.FullName, this.ExpectedType.FullName, element.Name), element);
            }
        }

        protected Type ExpectedType
        {
            get
            {
                return this._expectedType;
            }
        }
    }
}

