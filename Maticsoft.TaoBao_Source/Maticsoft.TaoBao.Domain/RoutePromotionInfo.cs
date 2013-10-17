namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RoutePromotionInfo : TopObject
    {
        [XmlElement("base_promotion_id")]
        public string BasePromotionId { get; set; }

        [XmlElement("promotion_description")]
        public string PromotionDescription { get; set; }

        [XmlElement("promotion_url")]
        public string PromotionUrl { get; set; }

        [XmlElement("unbase_promotion")]
        public bool UnbasePromotion { get; set; }
    }
}

