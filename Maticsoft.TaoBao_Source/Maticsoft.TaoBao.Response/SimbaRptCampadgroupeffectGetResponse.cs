namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptCampadgroupeffectGetResponse : TopResponse
    {
        [XmlElement("rpt_campadgroup_effect_list")]
        public string RptCampadgroupEffectList { get; set; }
    }
}

