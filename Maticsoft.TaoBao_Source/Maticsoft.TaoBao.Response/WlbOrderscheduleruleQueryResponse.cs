namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbOrderscheduleruleQueryResponse : TopResponse
    {
        [XmlArray("order_schedule_rule_list"), XmlArrayItem("wlb_order_schedule_rule")]
        public List<WlbOrderScheduleRule> OrderScheduleRuleList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

