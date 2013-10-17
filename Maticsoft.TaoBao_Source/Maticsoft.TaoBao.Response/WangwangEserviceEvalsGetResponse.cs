namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceEvalsGetResponse : TopResponse
    {
        [XmlElement("result_code")]
        public long ResultCode { get; set; }

        [XmlElement("result_count")]
        public long ResultCount { get; set; }

        [XmlArrayItem("eval_detail"), XmlArray("staff_eval_details")]
        public List<EvalDetail> StaffEvalDetails { get; set; }
    }
}

