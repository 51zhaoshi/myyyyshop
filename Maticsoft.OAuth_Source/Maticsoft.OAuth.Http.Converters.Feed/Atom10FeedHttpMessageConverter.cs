namespace Maticsoft.OAuth.Http.Converters.Feed
{
    using System;
    using System.ServiceModel.Syndication;
    using System.Xml;

    public class Atom10FeedHttpMessageConverter : AbstractFeedHttpMessageConverter
    {
        public Atom10FeedHttpMessageConverter() : base(new MediaType[] { new MediaType("application", "atom+xml") })
        {
        }

        protected override void WriteXml(XmlWriter xmlWriter, object content)
        {
            if (content is SyndicationFeed)
            {
                (content as SyndicationFeed).SaveAsAtom10(xmlWriter);
            }
            else if (content is SyndicationItem)
            {
                (content as SyndicationItem).SaveAsAtom10(xmlWriter);
            }
        }
    }
}

