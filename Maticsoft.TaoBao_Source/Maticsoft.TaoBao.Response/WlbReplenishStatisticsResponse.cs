namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbReplenishStatisticsResponse : TopResponse
    {
        [XmlArray("replenish_list"), XmlArrayItem("wlb_replenish")]
        public List<WlbReplenish> ReplenishList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

