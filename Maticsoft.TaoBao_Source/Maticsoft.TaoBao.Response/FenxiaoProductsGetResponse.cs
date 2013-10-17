namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoProductsGetResponse : TopResponse
    {
        [XmlArray("products"), XmlArrayItem("fenxiao_product")]
        public List<FenxiaoProduct> Products { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

