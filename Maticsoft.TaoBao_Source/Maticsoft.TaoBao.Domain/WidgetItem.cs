namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WidgetItem : TopObject
    {
        [XmlElement("add_to_cart")]
        public bool AddToCart { get; set; }

        [XmlElement("approve_status")]
        public string ApproveStatus { get; set; }

        [XmlElement("click_url")]
        public string ClickUrl { get; set; }

        [XmlElement("is_mall")]
        public bool IsMall { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlArray("item_pics"), XmlArrayItem("string")]
        public List<string> ItemPics { get; set; }

        [XmlArray("item_promotion_data"), XmlArrayItem("promotion_in_item")]
        public List<PromotionInItem> ItemPromotionData { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlArrayItem("promotion_in_shop"), XmlArray("shop_promotion_data")]
        public List<PromotionInShop> ShopPromotionData { get; set; }

        [XmlArray("sku_props"), XmlArrayItem("widget_sku_props")]
        public List<WidgetSkuProps> SkuProps { get; set; }

        [XmlArrayItem("widget_sku"), XmlArray("skus")]
        public List<WidgetSku> Skus { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

