namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ShoprecommendShopsGetResponse : TopResponse
    {
        [XmlArrayItem("favorite_shop"), XmlArray("favorite_shops")]
        public List<FavoriteShop> FavoriteShops { get; set; }
    }
}

