namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AreasGetResponse : TopResponse
    {
        [XmlArrayItem("area"), XmlArray("areas")]
        public List<Area> Areas { get; set; }
    }
}

