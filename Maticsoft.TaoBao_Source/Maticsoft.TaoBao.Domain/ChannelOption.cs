namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ChannelOption : TopObject
    {
        [XmlElement("channel_id")]
        public long ChannelId { get; set; }

        [XmlElement("is_nonsearch")]
        public bool IsNonsearch { get; set; }

        [XmlElement("is_search")]
        public bool IsSearch { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("traffic_name")]
        public string TrafficName { get; set; }

        [XmlElement("traffic_type")]
        public string TrafficType { get; set; }
    }
}

