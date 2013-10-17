namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class HotelImage : TopObject
    {
        [XmlElement("hid")]
        public long Hid { get; set; }

        [XmlElement("pic")]
        public string Pic { get; set; }
    }
}

