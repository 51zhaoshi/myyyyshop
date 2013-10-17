namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCreativeidsChangedGetResponse : TopResponse
    {
        [XmlArrayItem("number"), XmlArray("changed_creative_ids")]
        public List<long> ChangedCreativeIds { get; set; }
    }
}

