namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Place : TopObject
    {
        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("last_update_time")]
        public string LastUpdateTime { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("place_id")]
        public long PlaceId { get; set; }
    }
}

