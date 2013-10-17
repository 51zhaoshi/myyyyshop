namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Location : TopObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("district")]
        public string District { get; set; }

        [XmlElement("state")]
        public string State { get; set; }

        [XmlElement("zip")]
        public string Zip { get; set; }
    }
}

