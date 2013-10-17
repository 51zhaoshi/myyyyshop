namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallBrandcatSalesproGetResponse : TopResponse
    {
        [XmlArray("cat_brand_sale_props"), XmlArrayItem("cat_brand_sale_prop")]
        public List<CatBrandSaleProp> CatBrandSaleProps { get; set; }
    }
}

