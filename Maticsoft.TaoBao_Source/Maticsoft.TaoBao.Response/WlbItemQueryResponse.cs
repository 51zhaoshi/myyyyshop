namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemQueryResponse : TopResponse
    {
        [XmlArray("item_list"), XmlArrayItem("wlb_item")]
        public List<WlbItem> ItemList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

