namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupAdgroupcatmatchsGetResponse : TopResponse
    {
        [XmlArray("adgroup_catmatch_list"), XmlArrayItem("a_d_group_catmatch")]
        public List<ADGroupCatmatch> AdgroupCatmatchList { get; set; }
    }
}

