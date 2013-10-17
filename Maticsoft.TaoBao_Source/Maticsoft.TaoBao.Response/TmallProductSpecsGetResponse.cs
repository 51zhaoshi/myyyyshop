namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallProductSpecsGetResponse : TopResponse
    {
        [XmlArray("product_specs"), XmlArrayItem("product_spec")]
        public List<ProductSpec> ProductSpecs { get; set; }
    }
}

