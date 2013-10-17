namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ProductCat : TopObject
    {
        [XmlElement("cost_percent_agent")]
        public string CostPercentAgent { get; set; }

        [XmlElement("cost_percent_dealer")]
        public string CostPercentDealer { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("product_num")]
        public long ProductNum { get; set; }

        [XmlElement("retail_high_percent")]
        public string RetailHighPercent { get; set; }

        [XmlElement("retail_low_percent")]
        public string RetailLowPercent { get; set; }
    }
}

