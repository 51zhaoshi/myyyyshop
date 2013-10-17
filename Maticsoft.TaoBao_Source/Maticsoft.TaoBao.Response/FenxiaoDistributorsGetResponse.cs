namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoDistributorsGetResponse : TopResponse
    {
        [XmlArrayItem("distributor"), XmlArray("distributors")]
        public List<Distributor> Distributors { get; set; }
    }
}

