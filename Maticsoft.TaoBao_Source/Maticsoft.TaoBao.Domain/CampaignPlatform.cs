namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CampaignPlatform : TopObject
    {
        [XmlElement("campaign_id")]
        public long CampaignId { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlArrayItem("number"), XmlArray("nonsearch_channels")]
        public List<long> NonsearchChannels { get; set; }

        [XmlElement("outside_discount")]
        public long OutsideDiscount { get; set; }

        [XmlArray("search_channels"), XmlArrayItem("number")]
        public List<long> SearchChannels { get; set; }
    }
}

