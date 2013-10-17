namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CompanyRouteSummary : TopObject
    {
        [XmlElement("company_code")]
        public string CompanyCode { get; set; }

        [XmlElement("company_id")]
        public string CompanyId { get; set; }

        [XmlElement("company_name")]
        public string CompanyName { get; set; }

        [XmlElement("route_counts")]
        public long RouteCounts { get; set; }
    }
}

