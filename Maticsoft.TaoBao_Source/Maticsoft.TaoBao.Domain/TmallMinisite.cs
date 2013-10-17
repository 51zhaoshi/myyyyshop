namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TmallMinisite : TopObject
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("promotions")]
        public string Promotions { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("type")]
        public long Type { get; set; }
    }
}

