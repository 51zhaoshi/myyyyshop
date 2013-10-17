namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ProductsSearchResponse : TopResponse
    {
        [XmlArrayItem("product"), XmlArray("products")]
        public List<Product> Products { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

