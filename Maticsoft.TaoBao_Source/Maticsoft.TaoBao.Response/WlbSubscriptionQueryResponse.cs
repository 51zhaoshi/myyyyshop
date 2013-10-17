namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbSubscriptionQueryResponse : TopResponse
    {
        [XmlArrayItem("wlb_seller_subscription"), XmlArray("seller_subscription_list")]
        public List<WlbSellerSubscription> SellerSubscriptionList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

