namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FavoriteSearchResponse : TopResponse
    {
        [XmlArrayItem("collect_item"), XmlArray("collect_items")]
        public List<CollectItem> CollectItems { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

