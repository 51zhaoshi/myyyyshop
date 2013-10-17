namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class KfcSearchResult : TopObject
    {
        [XmlElement("content")]
        public string Content { get; set; }

        [XmlElement("level")]
        public string Level { get; set; }

        [XmlElement("matched")]
        public bool Matched { get; set; }
    }
}

