namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightWordsanalysisGetResponse : TopResponse
    {
        [XmlArrayItem("i_n_word_analysis"), XmlArray("in_word_analyses")]
        public List<INWordAnalysis> InWordAnalyses { get; set; }
    }
}

