namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureUpdateResponse : TopResponse
    {
        [XmlElement("done")]
        public bool Done { get; set; }
    }
}

