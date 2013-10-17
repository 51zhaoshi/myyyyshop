namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AreaOption : TopObject
    {
        [XmlElement("area_id")]
        public long AreaId { get; set; }

        [XmlElement("level")]
        public long Level { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parent_id")]
        public long ParentId { get; set; }
    }
}

