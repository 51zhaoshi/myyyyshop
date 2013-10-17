namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightCatstopwordGetResponse : TopResponse
    {
        [XmlArrayItem("string"), XmlArray("top_words")]
        public List<string> TopWords { get; set; }
    }
}

