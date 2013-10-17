namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ADGroupCatMatchPage : TopObject
    {
        [XmlArrayItem("a_d_group_catmatch"), XmlArray("adgroup_catmatch_list")]
        public List<ADGroupCatmatch> AdgroupCatmatchList { get; set; }

        [XmlElement("page_no")]
        public long PageNo { get; set; }

        [XmlElement("page_size")]
        public long PageSize { get; set; }

        [XmlElement("total_item")]
        public long TotalItem { get; set; }
    }
}

