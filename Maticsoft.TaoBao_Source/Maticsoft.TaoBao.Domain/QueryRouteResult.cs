namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class QueryRouteResult : TopObject
    {
        [XmlArray("company_route_summarys"), XmlArrayItem("company_route_summary")]
        public List<CompanyRouteSummary> CompanyRouteSummarys { get; set; }

        [XmlElement("is_turn_level")]
        public bool IsTurnLevel { get; set; }

        [XmlElement("pages_route_details")]
        public RouteAlpPage PagesRouteDetails { get; set; }

        [XmlArrayItem("route_vas_info"), XmlArray("route_vas")]
        public List<RouteVasInfo> RouteVas { get; set; }

        [XmlElement("total_corps")]
        public long TotalCorps { get; set; }

        [XmlElement("total_routes")]
        public long TotalRoutes { get; set; }
    }
}

