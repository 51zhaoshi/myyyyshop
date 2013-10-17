namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FenxiaoSku : TopObject
    {
        [XmlElement("cost_price")]
        public string CostPrice { get; set; }

        [XmlElement("dealer_cost_price")]
        public string DealerCostPrice { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        [XmlElement("properties")]
        public string Properties { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("standard_price")]
        public string StandardPrice { get; set; }
    }
}

