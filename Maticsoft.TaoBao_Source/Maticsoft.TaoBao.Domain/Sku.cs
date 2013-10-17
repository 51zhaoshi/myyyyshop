namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Sku : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("properties")]
        public string Properties { get; set; }

        [XmlElement("properties_name")]
        public string PropertiesName { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("sku_id")]
        public long SkuId { get; set; }

        [XmlElement("sku_spec_id")]
        public long SkuSpecId { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}

