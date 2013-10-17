namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptDemographiceffectGetResponse : TopResponse
    {
        [XmlElement("rpt_demographic_effect")]
        public string RptDemographicEffect { get; set; }
    }
}

