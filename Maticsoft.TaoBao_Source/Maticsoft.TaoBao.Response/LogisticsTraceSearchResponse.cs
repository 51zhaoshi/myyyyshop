namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsTraceSearchResponse : TopResponse
    {
        [XmlElement("company_name")]
        public string CompanyName { get; set; }

        [XmlElement("out_sid")]
        public string OutSid { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlArrayItem("transit_step_info"), XmlArray("trace_list")]
        public List<TransitStepInfo> TraceList { get; set; }
    }
}

