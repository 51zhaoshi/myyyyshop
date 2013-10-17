namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Shop : TopObject
    {
        [XmlElement("all_count")]
        public long AllCount { get; set; }

        [XmlElement("bulletin")]
        public string Bulletin { get; set; }

        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        [XmlElement("remain_count")]
        public long RemainCount { get; set; }

        [XmlElement("shop_score")]
        public Maticsoft.TaoBao.Domain.ShopScore ShopScore { get; set; }

        [XmlElement("sid")]
        public long Sid { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("used_count")]
        public long UsedCount { get; set; }
    }
}

