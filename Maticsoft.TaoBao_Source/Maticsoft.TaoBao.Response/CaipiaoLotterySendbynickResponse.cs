namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CaipiaoLotterySendbynickResponse : TopResponse
    {
        [XmlElement("send_result")]
        public bool SendResult { get; set; }
    }
}

