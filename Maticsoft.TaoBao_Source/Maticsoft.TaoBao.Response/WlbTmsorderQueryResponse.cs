namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbTmsorderQueryResponse : TopResponse
    {
        [XmlArray("tms_order_list"), XmlArrayItem("wlb_tms_order")]
        public List<WlbTmsOrder> TmsOrderList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

