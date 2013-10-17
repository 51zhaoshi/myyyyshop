namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TaobaokeItemsDetailGetResponse : TopResponse
    {
        [XmlArrayItem("taobaoke_item_detail"), XmlArray("taobaoke_item_details")]
        public List<TaobaokeItemDetail> TaobaokeItemDetails { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

