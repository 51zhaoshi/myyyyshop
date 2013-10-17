namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PicUrl : TopObject
    {
        [XmlElement("url")]
        public string Url { get; set; }
    }
}

