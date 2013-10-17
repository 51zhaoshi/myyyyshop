namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class RefundMessagesGetResponse : TopResponse
    {
        [XmlArrayItem("refund_message"), XmlArray("refund_messages")]
        public List<RefundMessage> RefundMessages { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

