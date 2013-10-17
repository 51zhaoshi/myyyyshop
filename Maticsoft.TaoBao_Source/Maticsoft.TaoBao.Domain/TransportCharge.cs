namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TransportCharge : TopObject
    {
        [XmlElement("cost")]
        public string Cost { get; set; }

        [XmlElement("cost_by")]
        public string CostBy { get; set; }

        [XmlElement("original_cost")]
        public string OriginalCost { get; set; }

        [XmlElement("saved_cost")]
        public string SavedCost { get; set; }
    }
}

