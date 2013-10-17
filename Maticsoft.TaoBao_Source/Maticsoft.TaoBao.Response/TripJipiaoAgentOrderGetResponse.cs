namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TripJipiaoAgentOrderGetResponse : TopResponse
    {
        [XmlArray("orders"), XmlArrayItem("at_order")]
        public List<AtOrder> Orders { get; set; }
    }
}

