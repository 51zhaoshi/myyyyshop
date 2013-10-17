namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LimitDiscountDetail : TopObject
    {
        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("item_discount")]
        public string ItemDiscount { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("limit_discount_name")]
        public string LimitDiscountName { get; set; }

        [XmlElement("limit_num")]
        public long LimitNum { get; set; }

        [XmlElement("start_time")]
        public string StartTime { get; set; }
    }
}

