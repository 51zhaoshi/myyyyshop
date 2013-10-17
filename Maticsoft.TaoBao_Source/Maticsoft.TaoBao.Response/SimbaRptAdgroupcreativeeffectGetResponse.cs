namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptAdgroupcreativeeffectGetResponse : TopResponse
    {
        [XmlElement("rpt_adgroupcreative_effect_list")]
        public string RptAdgroupcreativeEffectList { get; set; }
    }
}

