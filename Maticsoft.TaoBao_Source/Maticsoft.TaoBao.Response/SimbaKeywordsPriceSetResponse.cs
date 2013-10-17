namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaKeywordsPriceSetResponse : TopResponse
    {
        [XmlArray("keywords"), XmlArrayItem("keyword")]
        public List<Keyword> Keywords { get; set; }
    }
}

