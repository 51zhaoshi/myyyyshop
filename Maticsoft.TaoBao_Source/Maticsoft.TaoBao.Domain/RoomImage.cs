namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RoomImage : TopObject
    {
        [XmlElement("all_images")]
        public string AllImages { get; set; }

        [XmlElement("gid")]
        public long Gid { get; set; }

        [XmlElement("image")]
        public string Image { get; set; }

        [XmlElement("position")]
        public long Position { get; set; }
    }
}

