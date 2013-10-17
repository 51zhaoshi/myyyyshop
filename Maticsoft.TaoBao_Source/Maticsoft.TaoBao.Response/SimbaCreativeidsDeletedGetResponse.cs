namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCreativeidsDeletedGetResponse : TopResponse
    {
        [XmlArrayItem("number"), XmlArray("deleted_creative_ids")]
        public List<long> DeletedCreativeIds { get; set; }
    }
}

