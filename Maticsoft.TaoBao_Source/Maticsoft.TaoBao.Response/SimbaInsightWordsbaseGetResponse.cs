namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightWordsbaseGetResponse : TopResponse
    {
        [XmlArray("in_word_bases"), XmlArrayItem("i_n_word_base")]
        public List<INWordBase> InWordBases { get; set; }
    }
}

