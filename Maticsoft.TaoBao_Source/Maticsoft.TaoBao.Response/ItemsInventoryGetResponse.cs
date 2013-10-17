namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemsInventoryGetResponse : TopResponse
    {
        [XmlArrayItem("item"), XmlArray("items")]
        public List<Item> Items { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

