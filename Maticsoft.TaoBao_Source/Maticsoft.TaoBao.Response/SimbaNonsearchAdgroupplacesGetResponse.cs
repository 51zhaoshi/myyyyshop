namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaNonsearchAdgroupplacesGetResponse : TopResponse
    {
        [XmlArrayItem("a_d_group_place"), XmlArray("adgroup_place_list")]
        public List<ADGroupPlace> AdgroupPlaceList { get; set; }
    }
}

