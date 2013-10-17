namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelOrderGetResponse : TopResponse
    {
        [XmlElement("hotel_order")]
        public Maticsoft.TaoBao.Domain.HotelOrder HotelOrder { get; set; }
    }
}

