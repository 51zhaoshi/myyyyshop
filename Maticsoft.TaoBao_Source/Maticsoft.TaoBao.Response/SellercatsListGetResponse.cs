namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SellercatsListGetResponse : TopResponse
    {
        [XmlArrayItem("seller_cat"), XmlArray("seller_cats")]
        public List<SellerCat> SellerCats { get; set; }
    }
}

