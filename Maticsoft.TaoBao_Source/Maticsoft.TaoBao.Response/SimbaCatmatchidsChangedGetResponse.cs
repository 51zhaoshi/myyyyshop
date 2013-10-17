namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCatmatchidsChangedGetResponse : TopResponse
    {
        [XmlArrayItem("number"), XmlArray("changed_catmatch_ids")]
        public List<long> ChangedCatmatchIds { get; set; }
    }
}

