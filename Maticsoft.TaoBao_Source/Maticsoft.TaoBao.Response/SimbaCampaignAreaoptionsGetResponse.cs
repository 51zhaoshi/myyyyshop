namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCampaignAreaoptionsGetResponse : TopResponse
    {
        [XmlArray("area_options"), XmlArrayItem("area_option")]
        public List<AreaOption> AreaOptions { get; set; }
    }
}

