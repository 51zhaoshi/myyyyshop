namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptCustbaseGetResponse : TopResponse
    {
        [XmlElement("rpt_cust_base_list")]
        public string RptCustBaseList { get; set; }
    }
}

