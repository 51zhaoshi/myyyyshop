namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SimbaItemPartition : TopObject
    {
        [XmlArrayItem("simba_item"), XmlArray("item_list")]
        public List<SimbaItem> ItemList { get; set; }

        [XmlElement("order_by")]
        public bool OrderBy { get; set; }

        [XmlElement("order_field")]
        public string OrderField { get; set; }

        [XmlElement("page_no")]
        public long PageNo { get; set; }

        [XmlElement("page_size")]
        public long PageSize { get; set; }

        [XmlElement("total_item")]
        public long TotalItem { get; set; }
    }
}

