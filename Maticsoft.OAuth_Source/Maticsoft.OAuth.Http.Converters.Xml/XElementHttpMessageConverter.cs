namespace Maticsoft.OAuth.Http.Converters.Xml
{
    using System;
    using System.Xml;
    using System.Xml.Linq;

    public class XElementHttpMessageConverter : AbstractXmlHttpMessageConverter
    {
        protected override T ReadXml<T>(XmlReader xmlReader) where T: class
        {
            return (XElement.Load(xmlReader) as T);
        }

        protected override bool Supports(Type type)
        {
            return type.Equals(typeof(XElement));
        }

        protected override void WriteXml(XmlWriter xmlWriter, object content)
        {
            (content as XElement).WriteTo(xmlWriter);
        }
    }
}

