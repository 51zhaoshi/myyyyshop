namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceStreamweigthsGetResponse : TopResponse
    {
        [XmlElement("result_code")]
        public long ResultCode { get; set; }

        [XmlElement("result_count")]
        public long ResultCount { get; set; }

        [XmlArrayItem("stream_weight"), XmlArray("staff_stream_weights")]
        public List<StreamWeight> StaffStreamWeights { get; set; }

        [XmlElement("total_weight")]
        public long TotalWeight { get; set; }
    }
}

