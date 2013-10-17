namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbItemBatch : TopObject
    {
        [XmlElement("batch_code")]
        public string BatchCode { get; set; }

        [XmlElement("creator")]
        public string Creator { get; set; }

        [XmlElement("defect_quantity")]
        public long DefectQuantity { get; set; }

        [XmlElement("due_date")]
        public string DueDate { get; set; }

        [XmlElement("gmt_create")]
        public string GmtCreate { get; set; }

        [XmlElement("gmt_modified")]
        public string GmtModified { get; set; }

        [XmlElement("guarantee_period")]
        public string GuaranteePeriod { get; set; }

        [XmlElement("guarantee_unit")]
        public long GuaranteeUnit { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("is_deleted")]
        public bool IsDeleted { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("last_modifier")]
        public string LastModifier { get; set; }

        [XmlElement("produce_area")]
        public string ProduceArea { get; set; }

        [XmlElement("produce_code")]
        public string ProduceCode { get; set; }

        [XmlElement("produce_date")]
        public string ProduceDate { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("receive_date")]
        public string ReceiveDate { get; set; }

        [XmlElement("remarks")]
        public string Remarks { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("store_code")]
        public string StoreCode { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }

        [XmlElement("version")]
        public long Version { get; set; }
    }
}

