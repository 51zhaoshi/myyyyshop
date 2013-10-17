namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptDemographicbaseGetResponse : TopResponse
    {
        [XmlElement("rpt_demographic_base")]
        public string RptDemographicBase { get; set; }
    }
}

