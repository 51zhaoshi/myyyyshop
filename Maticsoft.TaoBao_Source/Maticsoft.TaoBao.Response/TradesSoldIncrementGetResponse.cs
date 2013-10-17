namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TradesSoldIncrementGetResponse : TopResponse
    {
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }

        [XmlArray("trades"), XmlArrayItem("trade")]
        public List<Trade> Trades { get; set; }
    }
}

