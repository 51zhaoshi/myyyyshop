namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoOrdersGetResponse : TopResponse
    {
        [XmlArray("purchase_orders"), XmlArrayItem("purchase_order")]
        public List<PurchaseOrder> PurchaseOrders { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

