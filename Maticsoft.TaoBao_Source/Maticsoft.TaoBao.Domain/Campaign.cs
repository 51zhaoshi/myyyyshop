namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Campaign : TopObject
    {
        [XmlElement("campaign_id")]
        public long CampaignId { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("online_status")]
        public string OnlineStatus { get; set; }

        [XmlElement("settle_reason")]
        public string SettleReason { get; set; }

        [XmlElement("settle_status")]
        public string SettleStatus { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

