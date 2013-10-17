namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCustomersAuthorizedGetResponse : TopResponse
    {
        [XmlArray("nicks"), XmlArrayItem("string")]
        public List<string> Nicks { get; set; }
    }
}

