namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightCatsbaseGetResponse : TopResponse
    {
        [XmlArray("in_category_bases"), XmlArrayItem("i_n_category_base")]
        public List<INCategoryBase> InCategoryBases { get; set; }
    }
}

