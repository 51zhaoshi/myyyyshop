namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TransitStepInfo : TopObject
    {
        [XmlElement("status_desc")]
        public string StatusDesc { get; set; }

        [XmlElement("status_time")]
        public string StatusTime { get; set; }
    }
}

