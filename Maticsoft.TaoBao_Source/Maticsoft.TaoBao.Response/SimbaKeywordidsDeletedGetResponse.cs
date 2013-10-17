namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaKeywordidsDeletedGetResponse : TopResponse
    {
        [XmlArrayItem("number"), XmlArray("deleted_keyword_ids")]
        public List<long> DeletedKeywordIds { get; set; }
    }
}

