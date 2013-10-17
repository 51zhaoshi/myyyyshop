namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TaobaokeItemsGetResponse : TopResponse
    {
        [XmlArrayItem("taobaoke_item"), XmlArray("taobaoke_items")]
        public List<TaobaokeItem> TaobaokeItems { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

