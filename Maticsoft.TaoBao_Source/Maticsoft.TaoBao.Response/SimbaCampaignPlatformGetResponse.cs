namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCampaignPlatformGetResponse : TopResponse
    {
        [XmlElement("campaign_platform")]
        public Maticsoft.TaoBao.Domain.CampaignPlatform CampaignPlatform { get; set; }
    }
}

