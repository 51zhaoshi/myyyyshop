namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TradeOrderskuUpdateResponse : TopResponse
    {
        [XmlElement("order")]
        public Maticsoft.TaoBao.Domain.Order Order { get; set; }
    }
}

