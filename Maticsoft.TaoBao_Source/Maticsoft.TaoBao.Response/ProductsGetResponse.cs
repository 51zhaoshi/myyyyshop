namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ProductsGetResponse : TopResponse
    {
        [XmlArray("products"), XmlArrayItem("product")]
        public List<Product> Products { get; set; }
    }
}

