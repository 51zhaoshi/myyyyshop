namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class OrderAmount : TopObject
    {
        [XmlElement("adjust_fee")]
        public string AdjustFee { get; set; }

        [XmlElement("discount_fee")]
        public string DiscountFee { get; set; }

        [XmlElement("num")]
        public long Num { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("promotion_name")]
        public string PromotionName { get; set; }

        [XmlElement("sku_id")]
        public long SkuId { get; set; }

        [XmlElement("sku_properties_name")]
        public string SkuPropertiesName { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

