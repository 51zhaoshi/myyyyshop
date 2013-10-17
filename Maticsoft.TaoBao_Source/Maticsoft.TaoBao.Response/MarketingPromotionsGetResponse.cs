namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class MarketingPromotionsGetResponse : TopResponse
    {
        [XmlArrayItem("promotion"), XmlArray("promotions")]
        public List<Promotion> Promotions { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

