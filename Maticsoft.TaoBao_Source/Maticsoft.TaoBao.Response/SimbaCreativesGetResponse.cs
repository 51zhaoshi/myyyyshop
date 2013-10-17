namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCreativesGetResponse : TopResponse
    {
        [XmlArray("creatives"), XmlArrayItem("creative")]
        public List<Creative> Creatives { get; set; }
    }
}

