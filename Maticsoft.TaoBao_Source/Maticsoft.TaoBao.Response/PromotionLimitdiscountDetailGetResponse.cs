namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PromotionLimitdiscountDetailGetResponse : TopResponse
    {
        [XmlArray("item_discount_detail_list"), XmlArrayItem("limit_discount_detail")]
        public List<LimitDiscountDetail> ItemDiscountDetailList { get; set; }
    }
}

