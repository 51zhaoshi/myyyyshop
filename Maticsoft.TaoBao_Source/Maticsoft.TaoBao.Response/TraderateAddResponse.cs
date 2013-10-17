namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TraderateAddResponse : TopResponse
    {
        [XmlElement("trade_rate")]
        public Maticsoft.TaoBao.Domain.TradeRate TradeRate { get; set; }
    }
}

