namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CampaignArea : TopObject
    {
        [XmlElement("area")]
        public string Area { get; set; }

        [XmlElement("campaign_id")]
        public long CampaignId { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }
    }
}

