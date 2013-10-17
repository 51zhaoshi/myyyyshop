namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightCatsforecastGetResponse : TopResponse
    {
        [XmlArrayItem("i_n_category_top"), XmlArray("in_category_tops")]
        public List<INCategoryTop> InCategoryTops { get; set; }
    }
}

