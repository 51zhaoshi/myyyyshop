namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FavoriteShop : TopObject
    {
        [XmlElement("rate")]
        public long Rate { get; set; }

        [XmlElement("seller_id")]
        public long SellerId { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("shop_id")]
        public long ShopId { get; set; }

        [XmlElement("shop_name")]
        public string ShopName { get; set; }

        [XmlElement("shop_pic")]
        public string ShopPic { get; set; }

        [XmlElement("shop_url")]
        public string ShopUrl { get; set; }
    }
}

