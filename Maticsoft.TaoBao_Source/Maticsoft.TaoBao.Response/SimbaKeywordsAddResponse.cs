namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaKeywordsAddResponse : TopResponse
    {
        [XmlArrayItem("keyword"), XmlArray("keywords")]
        public List<Keyword> Keywords { get; set; }
    }
}

