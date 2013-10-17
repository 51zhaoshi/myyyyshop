namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TaobaokeShopsGetResponse : TopResponse
    {
        [XmlArrayItem("taobaoke_shop"), XmlArray("taobaoke_shops")]
        public List<TaobaokeShop> TaobaokeShops { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

