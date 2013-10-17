namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Coupon : TopObject
    {
        [XmlElement("condition")]
        public long Condition { get; set; }

        [XmlElement("coupon_id")]
        public long CouponId { get; set; }

        [XmlElement("create_channel")]
        public string CreateChannel { get; set; }

        [XmlElement("creat_time")]
        public string CreatTime { get; set; }

        [XmlElement("denominations")]
        public long Denominations { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }
    }
}

