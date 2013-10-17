namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureCategoryUpdateResponse : TopResponse
    {
        [XmlElement("done")]
        public bool Done { get; set; }
    }
}

