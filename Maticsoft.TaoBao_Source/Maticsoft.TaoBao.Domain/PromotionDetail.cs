namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PromotionDetail : TopObject
    {
        [XmlElement("discount_fee")]
        public string DiscountFee { get; set; }

        [XmlElement("gift_item_id")]
        public string GiftItemId { get; set; }

        [XmlElement("gift_item_name")]
        public string GiftItemName { get; set; }

        [XmlElement("gift_item_num")]
        public string GiftItemNum { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("promotion_desc")]
        public string PromotionDesc { get; set; }

        [XmlElement("promotion_id")]
        public string PromotionId { get; set; }

        [XmlElement("promotion_name")]
        public string PromotionName { get; set; }
    }
}

