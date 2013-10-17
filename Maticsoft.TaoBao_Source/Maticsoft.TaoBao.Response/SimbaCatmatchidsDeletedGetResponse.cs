namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCatmatchidsDeletedGetResponse : TopResponse
    {
        [XmlArrayItem("number"), XmlArray("deleted_catmatch_ids")]
        public List<long> DeletedCatmatchIds { get; set; }
    }
}

