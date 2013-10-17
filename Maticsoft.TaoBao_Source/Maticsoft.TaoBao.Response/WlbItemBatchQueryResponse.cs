namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemBatchQueryResponse : TopResponse
    {
        [XmlArrayItem("wlb_item_batch_inventory"), XmlArray("item_inventory_batch_list")]
        public List<WlbItemBatchInventory> ItemInventoryBatchList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

