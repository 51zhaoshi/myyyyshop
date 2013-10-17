namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightToplevelcatsGetResponse : TopResponse
    {
        [XmlArray("in_category_tops"), XmlArrayItem("i_n_category_top")]
        public List<INCategoryTop> InCategoryTops { get; set; }
    }
}

