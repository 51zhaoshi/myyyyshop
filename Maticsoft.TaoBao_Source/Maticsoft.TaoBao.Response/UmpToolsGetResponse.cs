namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpToolsGetResponse : TopResponse
    {
        [XmlArrayItem("string"), XmlArray("tools")]
        public List<string> Tools { get; set; }
    }
}

