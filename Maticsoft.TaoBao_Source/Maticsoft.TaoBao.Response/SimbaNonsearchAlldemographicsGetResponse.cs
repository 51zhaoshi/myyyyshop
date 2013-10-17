namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaNonsearchAlldemographicsGetResponse : TopResponse
    {
        [XmlArray("demographic_list"), XmlArrayItem("demographic")]
        public List<Demographic> DemographicList { get; set; }
    }
}

