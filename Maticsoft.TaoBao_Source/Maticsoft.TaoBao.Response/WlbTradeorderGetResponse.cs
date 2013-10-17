namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbTradeorderGetResponse : TopResponse
    {
        [XmlArray("wlb_order_list"), XmlArrayItem("wlb_order")]
        public List<WlbOrder> WlbOrderList { get; set; }
    }
}

