namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FavoriteItem : TopObject
    {
        [XmlElement("item_name")]
        public string ItemName { get; set; }

        [XmlElement("item_pictrue")]
        public string ItemPictrue { get; set; }

        [XmlElement("item_price")]
        public string ItemPrice { get; set; }

        [XmlElement("item_url")]
        public string ItemUrl { get; set; }

        [XmlElement("promotion_price")]
        public string PromotionPrice { get; set; }

        [XmlElement("sell_count")]
        public long SellCount { get; set; }

        [XmlElement("track_iid")]
        public string TrackIid { get; set; }
    }
}

