namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WangwangInfo : TopObject
    {
        [XmlElement("site")]
        public string Site { get; set; }

        [XmlElement("wangwang_id")]
        public string WangwangId { get; set; }
    }
}

