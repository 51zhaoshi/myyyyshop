namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupNonsearchpricesUpdateResponse : TopResponse
    {
        [XmlArray("adgroup_list"), XmlArrayItem("a_d_group")]
        public List<ADGroup> AdgroupList { get; set; }
    }
}

