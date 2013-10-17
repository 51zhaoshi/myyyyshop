namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCampaignChanneloptionsGetResponse : TopResponse
    {
        [XmlArray("channel_options"), XmlArrayItem("channel_option")]
        public List<ChannelOption> ChannelOptions { get; set; }
    }
}

