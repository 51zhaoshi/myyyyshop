namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class IncrementTradesGetResponse : TopResponse
    {
        [XmlArrayItem("notify_trade"), XmlArray("notify_trades")]
        public List<NotifyTrade> NotifyTrades { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

