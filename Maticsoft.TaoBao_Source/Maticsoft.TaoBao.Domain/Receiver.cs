namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Receiver : TopObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("district")]
        public string District { get; set; }

        [XmlElement("mobile_phone")]
        public string MobilePhone { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("state")]
        public string State { get; set; }

        [XmlElement("zip")]
        public string Zip { get; set; }
    }
}

