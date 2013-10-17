namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Feature : TopObject
    {
        [XmlElement("attr_key")]
        public string AttrKey { get; set; }

        [XmlElement("attr_value")]
        public string AttrValue { get; set; }
    }
}

