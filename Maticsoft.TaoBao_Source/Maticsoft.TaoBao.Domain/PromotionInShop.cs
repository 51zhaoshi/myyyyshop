namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PromotionInShop : TopObject
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("promotion_detail_desc")]
        public string PromotionDetailDesc { get; set; }

        [XmlElement("promotion_id")]
        public string PromotionId { get; set; }
    }
}

