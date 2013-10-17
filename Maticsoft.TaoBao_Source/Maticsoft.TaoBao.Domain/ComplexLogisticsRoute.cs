namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ComplexLogisticsRoute : TopObject
    {
        [XmlElement("carriage_info")]
        public RouteCarriageInfo CarriageInfo { get; set; }

        [XmlElement("company")]
        public FreightCompany Company { get; set; }

        [XmlElement("extenal_info")]
        public RouteExtenalInfo ExtenalInfo { get; set; }

        [XmlElement("from_area_id")]
        public long FromAreaId { get; set; }

        [XmlElement("from_city_name")]
        public string FromCityName { get; set; }

        [XmlElement("from_county_name")]
        public string FromCountyName { get; set; }

        [XmlElement("from_province_name")]
        public string FromProvinceName { get; set; }

        [XmlElement("promotion_info")]
        public RoutePromotionInfo PromotionInfo { get; set; }

        [XmlElement("route_code")]
        public string RouteCode { get; set; }

        [XmlElement("statistics_info")]
        public RouteStatisticsInfo StatisticsInfo { get; set; }

        [XmlElement("to_area_id")]
        public long ToAreaId { get; set; }

        [XmlElement("to_city_name")]
        public string ToCityName { get; set; }

        [XmlElement("to_county_name")]
        public string ToCountyName { get; set; }

        [XmlElement("to_province_name")]
        public string ToProvinceName { get; set; }
    }
}

