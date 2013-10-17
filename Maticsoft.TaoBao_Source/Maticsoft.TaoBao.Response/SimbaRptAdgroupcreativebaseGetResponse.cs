namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptAdgroupcreativebaseGetResponse : TopResponse
    {
        [XmlElement("rpt_adgroupcreative_base_list")]
        public string RptAdgroupcreativeBaseList { get; set; }
    }
}

