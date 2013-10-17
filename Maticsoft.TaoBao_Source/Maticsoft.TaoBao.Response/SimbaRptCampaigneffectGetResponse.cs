namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaRptCampaigneffectGetResponse : TopResponse
    {
        [XmlElement("rpt_campaign_effect_list")]
        public string RptCampaignEffectList { get; set; }
    }
}

