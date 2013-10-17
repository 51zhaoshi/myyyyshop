namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AtOrder : TopObject
    {
        [XmlElement("base_info")]
        public Maticsoft.TaoBao.Domain.BaseInfo BaseInfo { get; set; }

        [XmlElement("corp_info")]
        public Maticsoft.TaoBao.Domain.CorpInfo CorpInfo { get; set; }

        [XmlElement("extra")]
        public string Extra { get; set; }

        [XmlElement("itinerary")]
        public Maticsoft.TaoBao.Domain.Itinerary Itinerary { get; set; }

        [XmlArray("segment_infos"), XmlArrayItem("segment_info")]
        public List<SegmentInfo> SegmentInfos { get; set; }
    }
}

