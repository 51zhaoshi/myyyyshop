namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptAdgroupnonsearcheffectGetResponse : TopResponse
    {
        [XmlElement("rpt_adgroup_nonsearch_effect")]
        public string RptAdgroupNonsearchEffect { get; set; }
    }
}

