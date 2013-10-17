namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ADGroupPlace : TopObject
    {
        [XmlElement("adgroup_id")]
        public long AdgroupId { get; set; }

        [XmlElement("campaign_id")]
        public long CampaignId { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("is_default_price")]
        public long IsDefaultPrice { get; set; }

        [XmlElement("max_price")]
        public long MaxPrice { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("place_id")]
        public long PlaceId { get; set; }

        [XmlElement("place_name")]
        public string PlaceName { get; set; }
    }
}

