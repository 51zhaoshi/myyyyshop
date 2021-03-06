namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbInventorylogQueryResponse : TopResponse
    {
        [XmlArray("inventory_log_list"), XmlArrayItem("wlb_item_inventory_log")]
        public List<WlbItemInventoryLog> InventoryLogList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

