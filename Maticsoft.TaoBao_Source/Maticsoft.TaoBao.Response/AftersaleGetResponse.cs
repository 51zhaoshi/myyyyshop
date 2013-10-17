namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AftersaleGetResponse : TopResponse
    {
        [XmlArrayItem("after_sale"), XmlArray("after_sales")]
        public List<AfterSale> AfterSales { get; set; }
    }
}

