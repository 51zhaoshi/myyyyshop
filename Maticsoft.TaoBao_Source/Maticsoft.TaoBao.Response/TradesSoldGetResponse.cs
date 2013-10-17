namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TradesSoldGetResponse : TopResponse
    {
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }

        [XmlArrayItem("trade"), XmlArray("trades")]
        public List<Trade> Trades { get; set; }
    }
}

