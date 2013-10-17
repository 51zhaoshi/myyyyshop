namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelSoldTypesIncrementGetResponse : TopResponse
    {
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        [XmlArrayItem("room_type"), XmlArray("room_types")]
        public List<RoomType> RoomTypes { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

