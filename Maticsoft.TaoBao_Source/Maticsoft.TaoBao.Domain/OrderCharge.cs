namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class OrderCharge : TopObject
    {
        [XmlElement("original_total_cost")]
        public string OriginalTotalCost { get; set; }

        [XmlElement("other_cost")]
        public string OtherCost { get; set; }

        [XmlElement("total_cost")]
        public string TotalCost { get; set; }

        [XmlElement("total_saved_cost")]
        public string TotalSavedCost { get; set; }

        [XmlElement("transport_charge")]
        public Maticsoft.TaoBao.Domain.TransportCharge TransportCharge { get; set; }

        [XmlElement("vas_charge")]
        public LogisticsVasCharge VasCharge { get; set; }
    }
}

