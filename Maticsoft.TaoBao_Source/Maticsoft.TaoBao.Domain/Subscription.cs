namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Subscription : TopObject
    {
        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("topic")]
        public string Topic { get; set; }
    }
}

