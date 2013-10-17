namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class VasOrderSearchResponse : TopResponse
    {
        [XmlArrayItem("article_biz_order"), XmlArray("article_biz_orders")]
        public List<ArticleBizOrder> ArticleBizOrders { get; set; }

        [XmlElement("total_item")]
        public long TotalItem { get; set; }
    }
}

