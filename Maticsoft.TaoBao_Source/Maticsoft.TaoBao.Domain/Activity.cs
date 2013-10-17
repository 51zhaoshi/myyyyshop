namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Activity : TopObject
    {
        [XmlElement("activity_id")]
        public long ActivityId { get; set; }

        [XmlElement("activity_url")]
        public string ActivityUrl { get; set; }

        [XmlElement("applied_count")]
        public long AppliedCount { get; set; }

        [XmlElement("coupon_id")]
        public long CouponId { get; set; }

        [XmlElement("create_user")]
        public string CreateUser { get; set; }

        [XmlElement("person_limit_count")]
        public long PersonLimitCount { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

