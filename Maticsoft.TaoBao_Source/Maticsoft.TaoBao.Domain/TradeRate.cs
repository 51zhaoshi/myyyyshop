namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TradeRate : TopObject
    {
        [XmlElement("content")]
        public string Content { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("item_price")]
        public string ItemPrice { get; set; }

        [XmlElement("item_title")]
        public string ItemTitle { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("rated_nick")]
        public string RatedNick { get; set; }

        [XmlElement("reply")]
        public string Reply { get; set; }

        [XmlElement("result")]
        public string Result { get; set; }

        [XmlElement("role")]
        public string Role { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("valid_score")]
        public bool ValidScore { get; set; }
    }
}

