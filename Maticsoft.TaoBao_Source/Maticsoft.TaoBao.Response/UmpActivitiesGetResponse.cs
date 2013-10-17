namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpActivitiesGetResponse : TopResponse
    {
        [XmlArrayItem("string"), XmlArray("contents")]
        public List<string> Contents { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

