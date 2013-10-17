namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptAdgroupeffectGetResponse : TopResponse
    {
        [XmlElement("rpt_adgroup_effect_list")]
        public string RptAdgroupEffectList { get; set; }
    }
}

