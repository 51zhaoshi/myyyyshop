namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbNotifyMessagePageGetResponse : TopResponse
    {
        [XmlElement("total_count")]
        public long TotalCount { get; set; }

        [XmlArray("wlb_messages"), XmlArrayItem("wlb_message")]
        public List<WlbMessage> WlbMessages { get; set; }
    }
}

