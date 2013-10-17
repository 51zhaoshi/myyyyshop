namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCampaignAreaUpdateResponse : TopResponse
    {
        [XmlElement("campaign_area")]
        public Maticsoft.TaoBao.Domain.CampaignArea CampaignArea { get; set; }
    }
}

