namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RouteStatisticsInfo : TopObject
    {
        [XmlElement("evaluation_count")]
        public long EvaluationCount { get; set; }

        [XmlElement("evaluation_score")]
        public string EvaluationScore { get; set; }

        [XmlElement("from_network_count")]
        public long FromNetworkCount { get; set; }

        [XmlElement("to_network_count")]
        public long ToNetworkCount { get; set; }

        [XmlElement("trunk_route_order_count")]
        public long TrunkRouteOrderCount { get; set; }
    }
}

