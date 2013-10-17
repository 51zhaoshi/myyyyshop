namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbItemInventoryLog : TopObject
    {
        [XmlElement("batch_code")]
        public string BatchCode { get; set; }

        [XmlElement("gmt_create")]
        public string GmtCreate { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("invent_type")]
        public string InventType { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("op_type")]
        public string OpType { get; set; }

        [XmlElement("op_user_id")]
        public long OpUserId { get; set; }

        [XmlElement("order_code")]
        public string OrderCode { get; set; }

        [XmlElement("order_item_id")]
        public long OrderItemId { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("remark")]
        public string Remark { get; set; }

        [XmlElement("result_quantity")]
        public long ResultQuantity { get; set; }

        [XmlElement("store_code")]
        public string StoreCode { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

