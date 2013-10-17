namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TaobaokeItem : TopObject
    {
        [XmlElement("click_url")]
        public string ClickUrl { get; set; }

        [XmlElement("commission")]
        public string Commission { get; set; }

        [XmlElement("commission_num")]
        public string CommissionNum { get; set; }

        [XmlElement("commission_rate")]
        public string CommissionRate { get; set; }

        [XmlElement("commission_volume")]
        public string CommissionVolume { get; set; }

        [XmlElement("coupon_end_time")]
        public string CouponEndTime { get; set; }

        [XmlElement("coupon_price")]
        public string CouponPrice { get; set; }

        [XmlElement("coupon_rate")]
        public string CouponRate { get; set; }

        [XmlElement("coupon_start_time")]
        public string CouponStartTime { get; set; }

        [XmlElement("item_location")]
        public string ItemLocation { get; set; }

        [XmlElement("keyword_click_url")]
        public string KeywordClickUrl { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("seller_credit_score")]
        public long SellerCreditScore { get; set; }

        [XmlElement("seller_id")]
        public long SellerId { get; set; }

        [XmlElement("shop_click_url")]
        public string ShopClickUrl { get; set; }

        [XmlElement("shop_type")]
        public string ShopType { get; set; }

        [XmlElement("taobaoke_cat_click_url")]
        public string TaobaokeCatClickUrl { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("volume")]
        public long Volume { get; set; }
    }
}

