namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LimitDiscount : TopObject
    {
        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("item_num")]
        public long ItemNum { get; set; }

        [XmlElement("limit_discount_id")]
        public long LimitDiscountId { get; set; }

        [XmlElement("limit_discount_name")]
        public string LimitDiscountName { get; set; }

        [XmlElement("start_time")]
        public string StartTime { get; set; }
    }
}

