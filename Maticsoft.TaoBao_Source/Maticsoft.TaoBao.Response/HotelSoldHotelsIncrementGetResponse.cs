namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelSoldHotelsIncrementGetResponse : TopResponse
    {
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        [XmlArray("hotels"), XmlArrayItem("hotel")]
        public List<Hotel> Hotels { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

