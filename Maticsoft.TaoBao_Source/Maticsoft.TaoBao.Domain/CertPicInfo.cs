namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CertPicInfo : TopObject
    {
        [XmlElement("cert_type")]
        public long CertType { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }
    }
}

