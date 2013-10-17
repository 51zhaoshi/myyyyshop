namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemrecommendItemsGetResponse : TopResponse
    {
        [XmlArrayItem("favorite_item"), XmlArray("values")]
        public List<FavoriteItem> Values { get; set; }
    }
}

