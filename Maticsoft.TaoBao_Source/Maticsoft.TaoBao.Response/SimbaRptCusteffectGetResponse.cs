namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptCusteffectGetResponse : TopResponse
    {
        [XmlElement("rpt_cust_effect_list")]
        public string RptCustEffectList { get; set; }
    }
}

