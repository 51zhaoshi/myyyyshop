namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightCatsrelatedwordGetResponse : TopResponse
    {
        [XmlArrayItem("string"), XmlArray("related_words")]
        public List<string> RelatedWords { get; set; }
    }
}

