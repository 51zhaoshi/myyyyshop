namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TmallSearchItem : TopObject
    {
        [XmlElement("fast_post_fee")]
        public string FastPostFee { get; set; }

        [XmlElement("is_cod")]
        public long IsCod { get; set; }

        [XmlElement("is_promotion")]
        public bool IsPromotion { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("location")]
        public string Location { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("price_with_rate")]
        public string PriceWithRate { get; set; }

        [XmlElement("seller_loc")]
        public string SellerLoc { get; set; }

        [XmlElement("shipping")]
        public long Shipping { get; set; }

        [XmlElement("sold")]
        public string Sold { get; set; }

        [XmlElement("spu_id")]
        public long SpuId { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

