namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class NotifyItem : TopObject
    {
        [XmlElement("changed_fields")]
        public string ChangedFields { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("increment")]
        public long Increment { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num")]
        public long Num { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("sku_id")]
        public long SkuId { get; set; }

        [XmlElement("sku_num")]
        public long SkuNum { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

