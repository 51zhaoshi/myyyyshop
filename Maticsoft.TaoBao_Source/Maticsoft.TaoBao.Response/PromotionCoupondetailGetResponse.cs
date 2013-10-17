namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PromotionCoupondetailGetResponse : TopResponse
    {
        [XmlArrayItem("coupon_detail"), XmlArray("coupon_details")]
        public List<CouponDetail> CouponDetails { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

