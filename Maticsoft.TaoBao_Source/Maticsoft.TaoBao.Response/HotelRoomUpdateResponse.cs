namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelRoomUpdateResponse : TopResponse
    {
        [XmlElement("room")]
        public Maticsoft.TaoBao.Domain.Room Room { get; set; }
    }
}

