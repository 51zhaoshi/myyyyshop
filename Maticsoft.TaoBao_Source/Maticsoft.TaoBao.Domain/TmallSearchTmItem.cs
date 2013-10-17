namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TmallSearchTmItem : TopObject
    {
        [XmlElement("brand_id")]
        public long BrandId { get; set; }

        [XmlElement("brand_name")]
        public string BrandName { get; set; }

        [XmlElement("comment_num")]
        public string CommentNum { get; set; }

        [XmlElement("commission_rate")]
        public string CommissionRate { get; set; }

        [XmlElement("detail_url")]
        public string DetailUrl { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("promotion_price")]
        public string PromotionPrice { get; set; }

        [XmlElement("tag_hot")]
        public string TagHot { get; set; }

        [XmlElement("tag_lq")]
        public string TagLq { get; set; }

        [XmlElement("tag_new")]
        public string TagNew { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("track_iid")]
        public string TrackIid { get; set; }

        [XmlElement("volume")]
        public long Volume { get; set; }
    }
}

