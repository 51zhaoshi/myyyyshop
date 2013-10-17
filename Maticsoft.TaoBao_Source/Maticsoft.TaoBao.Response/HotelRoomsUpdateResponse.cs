namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelRoomsUpdateResponse : TopResponse
    {
        [XmlArrayItem("string"), XmlArray("gids")]
        public List<string> Gids { get; set; }
    }
}

