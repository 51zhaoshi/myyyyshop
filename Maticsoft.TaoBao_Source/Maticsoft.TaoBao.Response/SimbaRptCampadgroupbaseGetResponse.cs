namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptCampadgroupbaseGetResponse : TopResponse
    {
        [XmlElement("rpt_campadgroup_base_list")]
        public string RptCampadgroupBaseList { get; set; }
    }
}

