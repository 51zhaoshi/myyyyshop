namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptAdgroupbaseGetResponse : TopResponse
    {
        [XmlElement("rpt_adgroup_base_list")]
        public string RptAdgroupBaseList { get; set; }
    }
}

