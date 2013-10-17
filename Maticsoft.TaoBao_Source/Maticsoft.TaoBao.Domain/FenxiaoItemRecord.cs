namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FenxiaoItemRecord : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("distributor_id")]
        public long DistributorId { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlElement("trade_type")]
        public string TradeType { get; set; }
    }
}

