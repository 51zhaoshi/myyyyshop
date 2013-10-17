namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RecommendWord : TopObject
    {
        [XmlElement("average_price")]
        public string AveragePrice { get; set; }

        [XmlElement("pertinence")]
        public string Pertinence { get; set; }

        [XmlElement("pv")]
        public string Pv { get; set; }

        [XmlElement("word")]
        public string Word { get; set; }
    }
}

