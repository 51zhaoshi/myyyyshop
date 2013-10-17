namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptAdgroupkeywordbaseGetResponse : TopResponse
    {
        [XmlElement("rpt_adgroupkeyword_base_list")]
        public string RptAdgroupkeywordBaseList { get; set; }
    }
}

