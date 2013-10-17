namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class KeywordQscore : TopObject
    {
        [XmlElement("adgroup_id")]
        public long AdgroupId { get; set; }

        [XmlElement("campaign_id")]
        public long CampaignId { get; set; }

        [XmlElement("keyword_id")]
        public long KeywordId { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("qscore")]
        public string Qscore { get; set; }

        [XmlElement("word")]
        public string Word { get; set; }
    }
}

