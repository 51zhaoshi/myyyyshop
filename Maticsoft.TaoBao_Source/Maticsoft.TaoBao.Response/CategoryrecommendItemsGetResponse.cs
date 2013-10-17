namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CategoryrecommendItemsGetResponse : TopResponse
    {
        [XmlArrayItem("favorite_item"), XmlArray("favorite_items")]
        public List<FavoriteItem> FavoriteItems { get; set; }
    }
}

