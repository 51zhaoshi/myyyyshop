namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoDiscountsGetResponse : TopResponse
    {
        [XmlArrayItem("discount"), XmlArray("discounts")]
        public List<Discount> Discounts { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

