namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoProductImageUploadResponse : TopResponse
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("result")]
        public bool Result { get; set; }
    }
}

