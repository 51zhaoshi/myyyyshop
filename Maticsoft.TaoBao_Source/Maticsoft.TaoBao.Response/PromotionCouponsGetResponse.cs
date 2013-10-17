namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PromotionCouponsGetResponse : TopResponse
    {
        [XmlArrayItem("coupon"), XmlArray("coupons")]
        public List<Coupon> Coupons { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

