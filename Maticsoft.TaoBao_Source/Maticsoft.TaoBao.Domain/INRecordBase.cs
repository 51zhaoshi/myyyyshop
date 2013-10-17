namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class INRecordBase : TopObject
    {
        [XmlElement("avg_price")]
        public long AvgPrice { get; set; }

        [XmlElement("click")]
        public long Click { get; set; }

        [XmlElement("competition")]
        public long Competition { get; set; }

        [XmlElement("ctr")]
        public string Ctr { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("pv")]
        public long Pv { get; set; }
    }
}

