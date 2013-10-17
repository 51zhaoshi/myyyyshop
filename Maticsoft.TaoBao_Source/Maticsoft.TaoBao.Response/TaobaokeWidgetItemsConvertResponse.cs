namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TaobaokeWidgetItemsConvertResponse : TopResponse
    {
        [XmlArray("taobaoke_items"), XmlArrayItem("taobaoke_item")]
        public List<TaobaokeItem> TaobaokeItems { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

