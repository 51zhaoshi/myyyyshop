namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbOrderitemPageGetResponse : TopResponse
    {
        [XmlArrayItem("wlb_order_item"), XmlArray("order_item_list")]
        public List<WlbOrderItem> OrderItemList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

