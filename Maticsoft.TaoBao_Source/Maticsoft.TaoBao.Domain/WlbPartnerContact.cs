namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbPartnerContact : TopObject
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }
    }
}

