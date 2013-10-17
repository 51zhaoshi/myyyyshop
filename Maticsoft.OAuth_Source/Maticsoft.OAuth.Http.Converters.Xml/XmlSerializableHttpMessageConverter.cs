namespace Maticsoft.OAuth.Http.Converters.Xml
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlSerializableHttpMessageConverter : AbstractXmlHttpMessageConverter
    {
        private Type[] _knownTypes;

        protected virtual XmlSerializer GetSerializer(Type type)
        {
            if (this._knownTypes == null)
            {
                return new XmlSerializer(type);
            }
            return new XmlSerializer(type, this._knownTypes);
        }

        protected override T ReadXml<T>(XmlReader xmlReader) where T: class
        {
            return (this.GetSerializer(typeof(T)).Deserialize(xmlReader) as T);
        }

        protected override bool Supports(Type type)
        {
            return true;
        }

        protected override void WriteXml(XmlWriter xmlWriter, object content)
        {
            this.GetSerializer(content.GetType()).Serialize(xmlWriter, content);
        }

        public Type[] KnownTypes
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
    }
}

