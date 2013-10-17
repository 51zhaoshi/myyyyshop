namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Msg : TopObject
    {
        [XmlElement("content")]
        public string Content { get; set; }

        [XmlElement("direction")]
        public long Direction { get; set; }

        [XmlElement("time")]
        public string Time { get; set; }
    }
}

