namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbInventoryDetailGetResponse : TopResponse
    {
        [XmlArray("inventory_list"), XmlArrayItem("wlb_inventory")]
        public List<WlbInventory> InventoryList { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }
    }
}

