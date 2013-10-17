namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaNonsearchAdgroupplacesUpdateResponse : TopResponse
    {
        [XmlArray("adgroup_place_list"), XmlArrayItem("a_d_group_place")]
        public List<ADGroupPlace> AdgroupPlaceList { get; set; }
    }
}

