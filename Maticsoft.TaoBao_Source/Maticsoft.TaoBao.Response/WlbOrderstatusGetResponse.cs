namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbOrderstatusGetResponse : TopResponse
    {
        [XmlArray("wlb_order_status"), XmlArrayItem("wlb_process_status")]
        public List<WlbProcessStatus> WlbOrderStatus { get; set; }
    }
}

