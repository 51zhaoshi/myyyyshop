namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ADGroup : TopObject
    {
        [XmlElement("adgroup_id")]
        public long AdgroupId { get; set; }

        [XmlElement("campaign_id")]
        public long CampaignId { get; set; }

        [XmlElement("category_ids")]
        public string CategoryIds { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("default_price")]
        public long DefaultPrice { get; set; }

        [XmlElement("is_nonsearch_default_price")]
        public bool IsNonsearchDefaultPrice { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("nonsearch_max_price")]
        public long NonsearchMaxPrice { get; set; }

        [XmlElement("nonsearch_status")]
        public long NonsearchStatus { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("offline_type")]
        public string OfflineType { get; set; }

        [XmlElement("online_status")]
        public string OnlineStatus { get; set; }

        [XmlElement("reason")]
        public string Reason { get; set; }
    }
}

