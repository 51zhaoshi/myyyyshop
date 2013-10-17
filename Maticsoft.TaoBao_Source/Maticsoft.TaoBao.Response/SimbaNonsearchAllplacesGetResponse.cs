namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaNonsearchAllplacesGetResponse : TopResponse
    {
        [XmlArrayItem("place"), XmlArray("place_list")]
        public List<Place> PlaceList { get; set; }
    }
}

