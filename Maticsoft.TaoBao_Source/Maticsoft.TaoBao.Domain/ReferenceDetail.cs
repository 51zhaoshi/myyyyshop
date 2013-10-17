namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ReferenceDetail : TopObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("reference_type")]
        public string ReferenceType { get; set; }
    }
}

