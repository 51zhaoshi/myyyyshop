namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpMbbsListGetResponse : TopResponse
    {
        [XmlArrayItem("string"), XmlArray("mbbs")]
        public List<string> Mbbs { get; set; }
    }
}

