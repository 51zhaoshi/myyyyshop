namespace Maticsoft.OAuth.Http.Converters.Xml
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Converters;
    using System;
    using System.Text;
    using System.Xml;

    public abstract class AbstractXmlHttpMessageConverter : AbstractHttpMessageConverter
    {
        protected static readonly Encoding DEFAULT_CHARSET = new UTF8Encoding(false);

        protected AbstractXmlHttpMessageConverter() : base(new MediaType[] { new MediaType("application", "xml"), new MediaType("text", "xml"), new MediaType("application", "*+xml") })
        {
        }

        protected AbstractXmlHttpMessageConverter(params MediaType[] supportedMediaTypes) : base(supportedMediaTypes)
        {
        }

        protected virtual XmlReaderSettings GetXmlReaderSettings()
        {
            return new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Auto, CloseInput = true, IgnoreProcessingInstructions = true, IgnoreWhitespace = true };
        }

        protected virtual XmlWriterSettings GetXmlWriterSettings()
        {
            return new XmlWriterSettings { CloseOutput = false, NewLineHandling = NewLineHandling.Entitize, OmitXmlDeclaration = true, CheckCharacters = false };
        }

        protected override T ReadInternal<T>(IHttpInputMessage message) where T: class
        {
            XmlReaderSettings xmlReaderSettings = this.GetXmlReaderSettings();
            using (XmlReader reader = XmlReader.Create(message.Body, xmlReaderSettings))
            {
                return this.ReadXml<T>(reader);
            }
        }

        protected abstract T ReadXml<T>(XmlReader xmlReader) where T: class;
        protected override void WriteInternal(object content, IHttpOutputMessage message)
        {
            Encoding contentTypeCharset = this.GetContentTypeCharset(message.Headers.ContentType, DEFAULT_CHARSET);
            XmlWriterSettings settings = this.GetXmlWriterSettings();
            settings.Encoding = contentTypeCharset;
            message.Body = delegate (Stream stream) {
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    this.WriteXml(writer, content);
                }
            };
        }

        protected abstract void WriteXml(XmlWriter xmlWriter, object content);
    }
}

