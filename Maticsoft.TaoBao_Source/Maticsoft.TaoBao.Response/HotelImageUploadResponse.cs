namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelImageUploadResponse : TopResponse
    {
        [XmlElement("hotel_image")]
        public Maticsoft.TaoBao.Domain.HotelImage HotelImage { get; set; }
    }
}

