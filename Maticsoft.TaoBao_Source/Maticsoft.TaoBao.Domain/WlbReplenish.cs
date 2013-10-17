namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbReplenish : TopObject
    {
        [XmlElement("estimate_value")]
        public string EstimateValue { get; set; }

        [XmlElement("history_value")]
        public string HistoryValue { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("retrieval_count")]
        public long RetrievalCount { get; set; }

        [XmlElement("sell_count")]
        public long SellCount { get; set; }

        [XmlElement("store_code")]
        public string StoreCode { get; set; }

        [XmlElement("transport_count")]
        public long TransportCount { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }

        [XmlElement("warn_count")]
        public long WarnCount { get; set; }
    }
}

