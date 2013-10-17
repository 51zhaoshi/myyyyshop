namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupNonsearchstatesUpdateResponse : TopResponse
    {
        [XmlArrayItem("a_d_group"), XmlArray("adgroup_list")]
        public List<ADGroup> AdgroupList { get; set; }
    }
}

