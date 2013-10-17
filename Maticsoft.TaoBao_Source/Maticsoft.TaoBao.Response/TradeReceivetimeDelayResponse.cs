namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TradeReceivetimeDelayResponse : TopResponse
    {
        [XmlElement("trade")]
        public Maticsoft.TaoBao.Domain.Trade Trade { get; set; }
    }
}

