namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaInsightWordscatsGetResponse : TopResponse
    {
        [XmlArrayItem("i_n_word_category"), XmlArray("in_word_categories")]
        public List<INWordCategory> InWordCategories { get; set; }
    }
}

