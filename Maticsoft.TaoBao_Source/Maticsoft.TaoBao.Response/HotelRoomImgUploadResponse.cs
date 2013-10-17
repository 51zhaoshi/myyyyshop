namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelRoomImgUploadResponse : TopResponse
    {
        [XmlElement("room_image")]
        public Maticsoft.TaoBao.Domain.RoomImage RoomImage { get; set; }
    }
}

