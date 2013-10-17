namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class HotelOrderFaceDealResponse : TopResponse
    {
        [XmlElement("result")]
        public string Result { get; set; }
    }
}

