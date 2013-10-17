namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbOrderScheduleRuleAddResponse : TopResponse
    {
        [XmlElement("schedule_rule_id")]
        public long ScheduleRuleId { get; set; }
    }
}

