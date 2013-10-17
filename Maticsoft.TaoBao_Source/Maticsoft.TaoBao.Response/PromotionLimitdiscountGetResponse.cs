namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PromotionLimitdiscountGetResponse : TopResponse
    {
        [XmlArrayItem("limit_discount"), XmlArray("limit_discount_list")]
        public List<LimitDiscount> LimitDiscountList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

