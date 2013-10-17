namespace Maticsoft.OAuth.Http.Converters.Xml
{
    using System;
    using System.Xml;

    public class XmlDocumentHttpMessageConverter : AbstractXmlHttpMessageConverter
    {
        protected override T ReadXml<T>(XmlReader xmlReader) where T: class
        {
            XmlDocument document = new XmlDocument();
            document.Load(xmlReader);
            return (document as T);
        }

        protected override bool Supports(Type type)
        {
            return type.Equals(typeof(XmlDocument));
        }

        protected override void WriteXml(XmlWriter xmlWriter, object content)
        {
            (content as XmlDocument).WriteTo(xmlWriter);
        }
    }
}

