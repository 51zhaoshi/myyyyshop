namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class StreamWeight : TopObject
    {
        [XmlElement("user")]
        public string User { get; set; }

        [XmlElement("weight")]
        public long Weight { get; set; }
    }
}

