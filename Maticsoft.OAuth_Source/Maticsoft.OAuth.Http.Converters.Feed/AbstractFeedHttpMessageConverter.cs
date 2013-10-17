namespace Maticsoft.OAuth.Http.Converters.Feed
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Converters.Xml;
    using System;
    using System.ServiceModel.Syndication;
    using System.Xml;

    public abstract class AbstractFeedHttpMessageConverter : AbstractXmlHttpMessageConverter
    {
        protected AbstractFeedHttpMessageConverter(params MediaType[] supportedMediaTypes) : base(supportedMediaTypes)
        {
        }

        protected override XmlReaderSettings GetXmlReaderSettings()
        {
            return new XmlReaderSettings { CloseInput = true, IgnoreProcessingInstructions = true, DtdProcessing = DtdProcessing.Ignore, XmlResolver = null };
        }

        protected override T ReadXml<T>(XmlReader xmlReader) where T: class
        {
            if (typeof(SyndicationFeed).Equals(typeof(T)))
            {
                return (SyndicationFeed.Load(xmlReader) as T);
            }
            if (typeof(SyndicationItem).Equals(typeof(T)))
            {
                return (SyndicationItem.Load(xmlReader) as T);
            }
            return default(T);
        }

        protected override bool Supports(Type type)
        {
            if (!type.Equals(typeof(SyndicationFeed)))
            {
                return type.Equals(typeof(SyndicationItem));
            }
            return true;
        }
    }
}

