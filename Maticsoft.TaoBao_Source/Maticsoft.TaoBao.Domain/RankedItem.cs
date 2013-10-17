namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RankedItem : TopObject
    {
        [XmlElement("link_url")]
        public string LinkUrl { get; set; }

        [XmlElement("max_price")]
        public string MaxPrice { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("order")]
        public long Order { get; set; }

        [XmlElement("rank_score")]
        public long RankScore { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

