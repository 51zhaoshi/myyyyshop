namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RoomType : TopObject
    {
        [XmlElement("alias_name")]
        public string AliasName { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("hid")]
        public long Hid { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("rid")]
        public long Rid { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }
    }
}

