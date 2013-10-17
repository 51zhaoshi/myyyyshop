namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallItemsDiscountSearchResponse : TopResponse
    {
        [XmlArrayItem("tmall_brand"), XmlArray("brand_list")]
        public List<TmallBrand> BrandList { get; set; }

        [XmlArray("cat_list"), XmlArrayItem("tmall_cat")]
        public List<TmallCat> CatList { get; set; }

        [XmlArrayItem("tmall_search_item"), XmlArray("item_list")]
        public List<TmallSearchItem> ItemList { get; set; }

        [XmlArrayItem("tmall_minisite"), XmlArray("minisite_list")]
        public List<TmallMinisite> MinisiteList { get; set; }

        [XmlElement("page")]
        public long Page { get; set; }

        [XmlElement("page_size")]
        public long PageSize { get; set; }

        [XmlElement("param_value")]
        public string ParamValue { get; set; }

        [XmlElement("search_url")]
        public string SearchUrl { get; set; }

        [XmlElement("total_page")]
        public long TotalPage { get; set; }

        [XmlElement("total_results")]
        public string TotalResults { get; set; }
    }
}

