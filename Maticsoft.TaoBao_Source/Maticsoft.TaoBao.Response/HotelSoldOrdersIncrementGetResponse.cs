namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelSoldOrdersIncrementGetResponse : TopResponse
    {
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        [XmlArray("hotel_orders"), XmlArrayItem("hotel_order")]
        public List<HotelOrder> HotelOrders { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

