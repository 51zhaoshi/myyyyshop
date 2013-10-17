namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PromotionInItem : TopObject
    {
        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("item_promo_price")]
        public string ItemPromoPrice { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("other_need")]
        public string OtherNeed { get; set; }

        [XmlElement("other_send")]
        public string OtherSend { get; set; }

        [XmlElement("promotion_id")]
        public string PromotionId { get; set; }

        [XmlArrayItem("string"), XmlArray("sku_id_list")]
        public List<string> SkuIdList { get; set; }

        [XmlArrayItem("price"), XmlArray("sku_price_list")]
        public List<string> SkuPriceList { get; set; }

        [XmlElement("start_time")]
        public string StartTime { get; set; }
    }
}

