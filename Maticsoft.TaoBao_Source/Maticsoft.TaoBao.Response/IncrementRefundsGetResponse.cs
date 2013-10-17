namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class IncrementRefundsGetResponse : TopResponse
    {
        [XmlArray("notify_refunds"), XmlArrayItem("notify_refund")]
        public List<NotifyRefund> NotifyRefunds { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

