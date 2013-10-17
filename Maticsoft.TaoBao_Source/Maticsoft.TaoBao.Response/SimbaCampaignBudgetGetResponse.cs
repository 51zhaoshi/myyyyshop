namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCampaignBudgetGetResponse : TopResponse
    {
        [XmlElement("campaign_budget")]
        public Maticsoft.TaoBao.Domain.CampaignBudget CampaignBudget { get; set; }
    }
}

