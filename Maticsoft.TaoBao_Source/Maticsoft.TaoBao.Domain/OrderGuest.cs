namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class OrderGuest : TopObject
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("person_pos")]
        public long PersonPos { get; set; }

        [XmlElement("room_pos")]
        public long RoomPos { get; set; }

        [XmlElement("tel")]
        public string Tel { get; set; }
    }
}

