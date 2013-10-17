namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCampaignUpdateResponse : TopResponse
    {
        [XmlElement("campaign")]
        public Maticsoft.TaoBao.Domain.Campaign Campaign { get; set; }
    }
}

