namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoTrademonitorGetResponse : TopResponse
    {
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

        [XmlArray("trade_monitors"), XmlArrayItem("trade_monitor")]
        public List<TradeMonitor> TradeMonitors { get; set; }
    }
}

