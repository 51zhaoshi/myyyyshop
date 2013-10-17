namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Demographic : TopObject
    {
        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("group_id")]
        public long GroupId { get; set; }

        [XmlElement("group_name")]
        public string GroupName { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("last_update_time")]
        public string LastUpdateTime { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}

