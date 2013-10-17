namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LogisticsVasItemCharge : TopObject
    {
        [XmlElement("cost")]
        public string Cost { get; set; }

        [XmlElement("original_cost")]
        public string OriginalCost { get; set; }

        [XmlElement("vas_code")]
        public string VasCode { get; set; }

        [XmlElement("vas_id")]
        public string VasId { get; set; }
    }
}

