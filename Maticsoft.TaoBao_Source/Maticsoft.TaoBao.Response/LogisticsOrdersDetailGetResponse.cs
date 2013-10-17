namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsOrdersDetailGetResponse : TopResponse
    {
        [XmlArrayItem("shipping"), XmlArray("shippings")]
        public List<Shipping> Shippings { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

