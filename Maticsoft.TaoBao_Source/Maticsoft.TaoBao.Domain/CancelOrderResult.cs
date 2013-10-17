namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CancelOrderResult : TopObject
    {
        [XmlElement("recreate_order_id")]
        public long RecreateOrderId { get; set; }
    }
}

