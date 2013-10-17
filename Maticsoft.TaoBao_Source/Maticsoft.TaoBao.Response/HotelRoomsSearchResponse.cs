namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelRoomsSearchResponse : TopResponse
    {
        [XmlArray("rooms"), XmlArrayItem("room")]
        public List<Room> Rooms { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

