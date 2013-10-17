namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PolicyDetail : TopObject
    {
        [XmlElement("change_rule")]
        public string ChangeRule { get; set; }

        [XmlElement("day_of_weeks")]
        public string DayOfWeeks { get; set; }

        [XmlElement("ei")]
        public string Ei { get; set; }

        [XmlElement("exclude_date")]
        public string ExcludeDate { get; set; }

        [XmlElement("memo")]
        public string Memo { get; set; }

        [XmlElement("office_id")]
        public string OfficeId { get; set; }

        [XmlElement("refund_rule")]
        public string RefundRule { get; set; }

        [XmlElement("reissue_rule")]
        public string ReissueRule { get; set; }

        [XmlElement("special_rule")]
        public string SpecialRule { get; set; }
    }
}

