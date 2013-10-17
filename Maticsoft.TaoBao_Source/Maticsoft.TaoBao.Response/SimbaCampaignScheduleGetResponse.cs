namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCampaignScheduleGetResponse : TopResponse
    {
        [XmlElement("campaign_schedule")]
        public Maticsoft.TaoBao.Domain.CampaignSchedule CampaignSchedule { get; set; }
    }
}

