namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbOrderPageGetResponse : TopResponse
    {
        [XmlArrayItem("wlb_order"), XmlArray("order_list")]
        public List<WlbOrder> OrderList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

