namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PromotionDisplayTop : TopObject
    {
        [XmlArray("promotion_in_item"), XmlArrayItem("promotion_in_item")]
        public List<Maticsoft.TaoBao.Domain.PromotionInItem> PromotionInItem { get; set; }

        [XmlArrayItem("promotion_in_shop"), XmlArray("promotion_in_shop")]
        public List<Maticsoft.TaoBao.Domain.PromotionInShop> PromotionInShop { get; set; }
    }
}

