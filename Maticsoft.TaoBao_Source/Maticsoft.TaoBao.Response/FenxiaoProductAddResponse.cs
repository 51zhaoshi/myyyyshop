namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoProductAddResponse : TopResponse
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("pid")]
        public long Pid { get; set; }
    }
}

