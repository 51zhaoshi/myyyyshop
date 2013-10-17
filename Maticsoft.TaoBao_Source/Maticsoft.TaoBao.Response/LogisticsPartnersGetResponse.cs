namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsPartnersGetResponse : TopResponse
    {
        [XmlArrayItem("logistics_partner"), XmlArray("logistics_partners")]
        public List<LogisticsPartner> LogisticsPartners { get; set; }
    }
}

