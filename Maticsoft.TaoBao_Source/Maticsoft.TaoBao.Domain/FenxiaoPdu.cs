namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FenxiaoPdu : TopObject
    {
        [XmlElement("distributor_id")]
        public long DistributorId { get; set; }

        [XmlElement("distributor_name")]
        public string DistributorName { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlElement("quantity_agent")]
        public long QuantityAgent { get; set; }

        [XmlElement("sku_properties")]
        public string SkuProperties { get; set; }
    }
}

