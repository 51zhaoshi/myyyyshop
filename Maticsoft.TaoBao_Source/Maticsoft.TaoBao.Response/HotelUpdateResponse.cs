namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelUpdateResponse : TopResponse
    {
        [XmlElement("hotel")]
        public Maticsoft.TaoBao.Domain.Hotel Hotel { get; set; }
    }
}

