namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoProductUpdateResponse : TopResponse
    {
        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("pid")]
        public long Pid { get; set; }
    }
}

