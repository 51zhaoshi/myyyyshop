namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlipayUserTradeSearchResponse : TopResponse
    {
        [XmlElement("total_pages")]
        public string TotalPages { get; set; }

        [XmlElement("total_results")]
        public string TotalResults { get; set; }

        [XmlArray("trade_records"), XmlArrayItem("trade_record")]
        public List<TradeRecord> TradeRecords { get; set; }
    }
}

