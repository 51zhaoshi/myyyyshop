namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SelectedItem : TopObject
    {
        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("item_score")]
        public string ItemScore { get; set; }

        [XmlElement("shop_id")]
        public long ShopId { get; set; }

        [XmlElement("track_iid")]
        public string TrackIid { get; set; }
    }
}

