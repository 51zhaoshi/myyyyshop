namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Creative : TopObject
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

        [XmlElement("creative_id")]
        public long CreativeId { get; set; }

        [XmlElement("img_url")]
        public string ImgUrl { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

