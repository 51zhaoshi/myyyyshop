namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TaobaokeShop : TopObject
    {
        [XmlElement("auction_count")]
        public long AuctionCount { get; set; }

        [XmlElement("click_url")]
        public string ClickUrl { get; set; }

        [XmlElement("commission_rate")]
        public string CommissionRate { get; set; }

        [XmlElement("seller_credit")]
        public string SellerCredit { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("shop_id")]
        public long ShopId { get; set; }

        [XmlElement("shop_title")]
        public string ShopTitle { get; set; }

        [XmlElement("shop_type")]
        public string ShopType { get; set; }

        [XmlElement("total_auction")]
        public string TotalAuction { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

