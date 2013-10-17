namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class INCategoryProperties : TopObject
    {
        [XmlElement("properties_desc")]
        public string PropertiesDesc { get; set; }

        [XmlElement("properties_id")]
        public long PropertiesId { get; set; }

        [XmlElement("properties_name")]
        public string PropertiesName { get; set; }
    }
}

