namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RouteVasInfo : TopObject
    {
        [XmlElement("route_code")]
        public string RouteCode { get; set; }

        [XmlArrayItem("logistics_vas"), XmlArray("vas_list")]
        public List<LogisticsVas> VasList { get; set; }
    }
}

