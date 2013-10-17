namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TaobaokeShopsRelateGetResponse : TopResponse
    {
        [XmlArray("taobaoke_shops"), XmlArrayItem("taobaoke_shop")]
        public List<TaobaokeShop> TaobaokeShops { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

