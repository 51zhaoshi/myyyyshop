namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TradeConfirmfeeGetResponse : TopResponse
    {
        [XmlElement("trade_confirm_fee")]
        public Maticsoft.TaoBao.Domain.TradeConfirmFee TradeConfirmFee { get; set; }
    }
}

