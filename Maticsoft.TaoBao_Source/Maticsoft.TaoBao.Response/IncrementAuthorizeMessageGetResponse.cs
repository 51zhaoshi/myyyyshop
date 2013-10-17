namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class IncrementAuthorizeMessageGetResponse : TopResponse
    {
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        [XmlArrayItem("string"), XmlArray("messages")]
        public List<string> Messages { get; set; }
    }
}

