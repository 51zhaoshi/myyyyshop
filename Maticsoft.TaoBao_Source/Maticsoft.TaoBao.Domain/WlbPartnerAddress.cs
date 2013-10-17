namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbPartnerAddress : TopObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("borough")]
        public string Borough { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("province")]
        public string Province { get; set; }

        [XmlElement("zip")]
        public string Zip { get; set; }
    }
}

