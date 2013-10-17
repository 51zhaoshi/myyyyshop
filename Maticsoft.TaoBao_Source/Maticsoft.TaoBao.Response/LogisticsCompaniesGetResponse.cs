namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsCompaniesGetResponse : TopResponse
    {
        [XmlArrayItem("logistics_company"), XmlArray("logistics_companies")]
        public List<LogisticsCompany> LogisticsCompanies { get; set; }
    }
}

