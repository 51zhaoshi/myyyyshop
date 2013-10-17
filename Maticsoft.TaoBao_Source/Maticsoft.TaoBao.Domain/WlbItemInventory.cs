namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbItemInventory : TopObject
    {
        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("lock_quantity")]
        public long LockQuantity { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("store_code")]
        public string StoreCode { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}

