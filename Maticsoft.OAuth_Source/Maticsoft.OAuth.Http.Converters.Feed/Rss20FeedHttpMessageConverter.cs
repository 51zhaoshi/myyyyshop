namespace Maticsoft.OAuth.Http.Converters.Feed
{
    using System;
    using System.ServiceModel.Syndication;
    using System.Xml;

    public class Rss20FeedHttpMessageConverter : AbstractFeedHttpMessageConverter
    {
        public Rss20FeedHttpMessageConverter() : base(new MediaType[] { new MediaType("application", "rss+xml") })
        {
        }

        protected override void WriteXml(XmlWriter xmlWriter, object content)
        {
            if (content is SyndicationFeed)
            {
                (content as SyndicationFeed).SaveAsRss20(xmlWriter);
            }
            else if (content is SyndicationItem)
            {
                (content as SyndicationItem).SaveAsRss20(xmlWriter);
            }
        }
    }
}

