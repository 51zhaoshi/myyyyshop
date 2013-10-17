namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class IncrementItemsGetResponse : TopResponse
    {
        [XmlArrayItem("notify_item"), XmlArray("notify_items")]
        public List<NotifyItem> NotifyItems { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

