namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WidgetCartInfo : TopObject
    {
        [XmlElement("cart_id")]
        public long CartId { get; set; }

        [XmlElement("delete_url")]
        public string DeleteUrl { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("item_url")]
        public string ItemUrl { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("sku")]
        public string Sku { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

