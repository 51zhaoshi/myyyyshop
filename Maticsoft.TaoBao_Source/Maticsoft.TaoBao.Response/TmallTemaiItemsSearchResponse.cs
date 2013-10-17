namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallTemaiItemsSearchResponse : TopResponse
    {
        [XmlArrayItem("tmall_search_tm_item"), XmlArray("item_list")]
        public List<TmallSearchTmItem> ItemList { get; set; }

        [XmlElement("page")]
        public long Page { get; set; }

        [XmlElement("page_size")]
        public long PageSize { get; set; }

        [XmlElement("total_page")]
        public long TotalPage { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

