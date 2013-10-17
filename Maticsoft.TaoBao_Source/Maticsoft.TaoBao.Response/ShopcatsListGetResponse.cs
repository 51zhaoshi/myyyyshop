namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ShopcatsListGetResponse : TopResponse
    {
        [XmlArray("shop_cats"), XmlArrayItem("shop_cat")]
        public List<ShopCat> ShopCats { get; set; }
    }
}

