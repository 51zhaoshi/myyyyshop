namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Keyword : TopObject
    {
        [XmlElement("adgroup_id")]
        public long AdgroupId { get; set; }

        [XmlElement("audit_desc")]
        public string AuditDesc { get; set; }

        [XmlElement("audit_status")]
        public string AuditStatus { get; set; }

        [XmlElement("campaign_id")]
        public long CampaignId { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("is_default_price")]
        public bool IsDefaultPrice { get; set; }

        [XmlElement("is_garbage")]
        public bool IsGarbage { get; set; }

        [XmlElement("keyword_id")]
        public long KeywordId { get; set; }

        [XmlElement("match_scope")]
        public string MatchScope { get; set; }

        [XmlElement("max_price")]
        public long MaxPrice { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("qscore")]
        public string Qscore { get; set; }

        [XmlElement("word")]
        public string Word { get; set; }
    }
}

