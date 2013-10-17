namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelOrdersSearchResponse : TopResponse
    {
        [XmlArrayItem("hotel_order"), XmlArray("hotel_orders")]
        public List<HotelOrder> HotelOrders { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

