namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptAdgroupkeywordeffectGetResponse : TopResponse
    {
        [XmlElement("rpt_adgroupkeyword_effect_list")]
        public string RptAdgroupkeywordEffectList { get; set; }
    }
}

