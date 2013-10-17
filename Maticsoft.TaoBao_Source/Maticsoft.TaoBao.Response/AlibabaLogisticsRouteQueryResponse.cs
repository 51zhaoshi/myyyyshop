namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlibabaLogisticsRouteQueryResponse : TopResponse
    {
        [XmlElement("query_route_result")]
        public Maticsoft.TaoBao.Domain.QueryRouteResult QueryRouteResult { get; set; }
    }
}

