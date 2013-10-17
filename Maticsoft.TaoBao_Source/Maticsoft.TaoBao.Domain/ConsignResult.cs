namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ConsignResult : TopObject
    {
        [XmlElement("logistics_id")]
        public string LogisticsId { get; set; }

        [XmlElement("order_id")]
        public long OrderId { get; set; }
    }
}

