namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbItemBatchInventory : TopObject
    {
        [XmlArray("item_batch"), XmlArrayItem("wlb_item_batch")]
        public List<WlbItemBatch> ItemBatch { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlArrayItem("wlb_item_inventory"), XmlArray("item_inventorys")]
        public List<WlbItemInventory> ItemInventorys { get; set; }

        [XmlElement("total_quantity")]
        public long TotalQuantity { get; set; }
    }
}

