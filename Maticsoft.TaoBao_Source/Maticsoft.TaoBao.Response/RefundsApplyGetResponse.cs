namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class RefundsApplyGetResponse : TopResponse
    {
        [XmlArrayItem("refund"), XmlArray("refunds")]
        public List<Refund> Refunds { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

