namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightCatsanalysisGetResponse : TopResponse
    {
        [XmlArrayItem("i_n_category_analysis"), XmlArray("in_category_analyses")]
        public List<INCategoryAnalysis> InCategoryAnalyses { get; set; }
    }
}

