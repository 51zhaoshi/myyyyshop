namespace Maticsoft.OAuth.Http.Converters.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml;

    public class DataContractHttpMessageConverter : AbstractXmlHttpMessageConverter
    {
        private IEnumerable<Type> _knownTypes;
        private bool _requiresAttribute;

        public DataContractHttpMessageConverter()
        {
        }

        public DataContractHttpMessageConverter(bool requiresAttribute)
        {
            this._requiresAttribute = requiresAttribute;
        }

        protected virtual DataContractSerializer GetSerializer(Type type)
        {
            if (this._knownTypes == null)
            {
                return new DataContractSerializer(type);
            }
            return new DataContractSerializer(type, this._knownTypes);
        }

        protected override T ReadXml<T>(XmlReader xmlReader) where T: class
        {
            return (this.GetSerializer(typeof(T)).ReadObject(xmlReader) as T);
        }

        protected override bool Supports(Type type)
        {
            if (this._requiresAttribute && (System.Attribute.GetCustomAttributes(type, typeof(DataContractAttribute), true).Length <= 0))
            {
                return (System.Attribute.GetCustomAttributes(type, typeof(CollectionDataContractAttribute), true).Length > 0);
            }
            return true;
        }

        protected override void WriteXml(XmlWriter xmlWriter, object content)
        {
            this.GetSerializer(content.GetType()).WriteObject(xmlWriter, content);
        }

        public IEnumerable<Type> KnownTypes
        {
            get
            {
                return this._knownTypes;
            }
            set
            {
                this._knownTypes = value;
            }
        }

        public bool RequiresAttribute
        {
            get
            {
                return this._requiresAttribute;
            }
        }
    }
}

