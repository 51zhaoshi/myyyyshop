namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PropImg : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("position")]
        public long Position { get; set; }

        [XmlElement("properties")]
        public string Properties { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
    }
}

