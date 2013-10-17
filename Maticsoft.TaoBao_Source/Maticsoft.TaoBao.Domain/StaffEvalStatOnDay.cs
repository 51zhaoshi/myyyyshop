namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class StaffEvalStatOnDay : TopObject
    {
        [XmlElement("eval_date")]
        public string EvalDate { get; set; }

        [XmlArray("staff_eval_stat_by_ids"), XmlArrayItem("staff_eval_stat_by_id")]
        public List<StaffEvalStatById> StaffEvalStatByIds { get; set; }
    }
}

