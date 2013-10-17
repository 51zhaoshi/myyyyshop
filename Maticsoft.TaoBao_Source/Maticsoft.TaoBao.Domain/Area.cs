namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Area : TopObject
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parent_id")]
        public long ParentId { get; set; }

        [XmlElement("type")]
        public long Type { get; set; }

        [XmlElement("zip")]
        public string Zip { get; set; }
    }
}

