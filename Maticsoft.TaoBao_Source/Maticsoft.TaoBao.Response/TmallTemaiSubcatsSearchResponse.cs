namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallTemaiSubcatsSearchResponse : TopResponse
    {
        [XmlArrayItem("tmall_tm_cat"), XmlArray("cat_list")]
        public List<TmallTmCat> CatList { get; set; }
    }
}

