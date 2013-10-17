namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class RefundRefuseResponse : TopResponse
    {
        [XmlElement("refund")]
        public Maticsoft.TaoBao.Domain.Refund Refund { get; set; }
    }
}

