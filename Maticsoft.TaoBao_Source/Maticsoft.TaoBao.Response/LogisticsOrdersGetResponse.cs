namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsOrdersGetResponse : TopResponse
    {
        [XmlArray("shippings"), XmlArrayItem("shipping")]
        public List<Shipping> Shippings { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

