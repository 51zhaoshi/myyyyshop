namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TraderatesGetResponse : TopResponse
    {
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }

        [XmlArray("trade_rates"), XmlArrayItem("trade_rate")]
        public List<TradeRate> TradeRates { get; set; }
    }
}

