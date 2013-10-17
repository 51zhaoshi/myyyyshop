namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaKeywordidsChangedGetResponse : TopResponse
    {
        [XmlArray("changed_keyword_ids"), XmlArrayItem("number")]
        public List<long> ChangedKeywordIds { get; set; }
    }
}

