namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCampaignsGetResponse : TopResponse
    {
        [XmlArrayItem("campaign"), XmlArray("campaigns")]
        public List<Campaign> Campaigns { get; set; }
    }
}

