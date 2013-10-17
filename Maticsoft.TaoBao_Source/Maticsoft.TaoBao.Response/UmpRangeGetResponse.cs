namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpRangeGetResponse : TopResponse
    {
        [XmlArrayItem("range"), XmlArray("ranges")]
        public List<Range> Ranges { get; set; }
    }
}

