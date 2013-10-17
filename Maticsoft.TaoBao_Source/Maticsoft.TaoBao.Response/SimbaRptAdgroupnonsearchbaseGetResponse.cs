namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptAdgroupnonsearchbaseGetResponse : TopResponse
    {
        [XmlElement("rpt_adgroup_nonsearch_base")]
        public string RptAdgroupNonsearchBase { get; set; }
    }
}

