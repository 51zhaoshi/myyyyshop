namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCreativesRecordGetResponse : TopResponse
    {
        [XmlArray("creativerecords"), XmlArrayItem("creative_record")]
        public List<CreativeRecord> Creativerecords { get; set; }
    }
}

