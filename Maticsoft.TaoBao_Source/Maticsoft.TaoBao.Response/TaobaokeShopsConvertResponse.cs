namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TaobaokeShopsConvertResponse : TopResponse
    {
        [XmlArrayItem("taobaoke_shop"), XmlArray("taobaoke_shops")]
        public List<TaobaokeShop> TaobaokeShops { get; set; }
    }
}

