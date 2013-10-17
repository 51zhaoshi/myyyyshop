namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelTypeNameGetResponse : TopResponse
    {
        [XmlElement("room_type")]
        public Maticsoft.TaoBao.Domain.RoomType RoomType { get; set; }
    }
}

