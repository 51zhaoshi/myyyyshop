namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TopFee : TopObject
    {
        [XmlElement("add_fee")]
        public string AddFee { get; set; }

        [XmlElement("add_standard")]
        public string AddStandard { get; set; }

        [XmlElement("destination")]
        public string Destination { get; set; }

        [XmlElement("service_type")]
        public string ServiceType { get; set; }

        [XmlElement("start_fee")]
        public string StartFee { get; set; }

        [XmlElement("start_standard")]
        public string StartStandard { get; set; }
    }
}

