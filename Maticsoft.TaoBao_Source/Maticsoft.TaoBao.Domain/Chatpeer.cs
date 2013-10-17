namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Chatpeer : TopObject
    {
        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("uid")]
        public string Uid { get; set; }
    }
}

