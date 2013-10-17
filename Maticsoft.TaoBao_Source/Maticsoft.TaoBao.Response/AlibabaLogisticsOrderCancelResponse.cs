namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlibabaLogisticsOrderCancelResponse : TopResponse
    {
        [XmlElement("cancel_order_result")]
        public Maticsoft.TaoBao.Domain.CancelOrderResult CancelOrderResult { get; set; }
    }
}

