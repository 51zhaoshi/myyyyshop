namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceEvaluationGetResponse : TopResponse
    {
        [XmlArray("staff_eval_stat_on_days"), XmlArrayItem("staff_eval_stat_on_day")]
        public List<StaffEvalStatOnDay> StaffEvalStatOnDays { get; set; }
    }
}

