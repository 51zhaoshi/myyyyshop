namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptCampaignbaseGetResponse : TopResponse
    {
        [XmlElement("rpt_campaign_base_list")]
        public string RptCampaignBaseList { get; set; }
    }
}

