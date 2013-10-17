namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class KeywordForecast : TopObject
    {
        [XmlElement("keyword_id")]
        public long KeywordId { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("price_click")]
        public string PriceClick { get; set; }

        [XmlElement("price_cust")]
        public string PriceCust { get; set; }

        [XmlElement("price_rank")]
        public string PriceRank { get; set; }

        [XmlElement("word")]
        public string Word { get; set; }
    }
}

