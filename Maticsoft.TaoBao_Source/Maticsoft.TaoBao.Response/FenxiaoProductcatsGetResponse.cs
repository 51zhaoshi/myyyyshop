namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoProductcatsGetResponse : TopResponse
    {
        [XmlArrayItem("product_cat"), XmlArray("productcats")]
        public List<ProductCat> Productcats { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

