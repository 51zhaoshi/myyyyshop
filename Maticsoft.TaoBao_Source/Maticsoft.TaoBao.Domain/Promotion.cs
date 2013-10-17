namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Promotion : TopObject
    {
        [XmlElement("decrease_num")]
        public long DecreaseNum { get; set; }

        [XmlElement("discount_type")]
        public string DiscountType { get; set; }

        [XmlElement("discount_value")]
        public string DiscountValue { get; set; }

        [XmlElement("end_date")]
        public string EndDate { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("promotion_desc")]
        public string PromotionDesc { get; set; }

        [XmlElement("promotion_id")]
        public long PromotionId { get; set; }

        [XmlElement("promotion_title")]
        public string PromotionTitle { get; set; }

        [XmlElement("start_date")]
        public string StartDate { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tag_id")]
        public long TagId { get; set; }
    }
}

