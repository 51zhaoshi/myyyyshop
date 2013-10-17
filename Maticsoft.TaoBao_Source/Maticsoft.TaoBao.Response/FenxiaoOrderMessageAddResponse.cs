namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoOrderMessageAddResponse : TopResponse
    {
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }
    }
}

