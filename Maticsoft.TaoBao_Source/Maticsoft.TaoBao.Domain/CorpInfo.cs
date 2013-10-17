namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CorpInfo : TopObject
    {
        [XmlElement("apply_name")]
        public string ApplyName { get; set; }

        [XmlElement("apply_no")]
        public string ApplyNo { get; set; }

        [XmlElement("apply_time")]
        public string ApplyTime { get; set; }

        [XmlElement("corpration_id")]
        public string CorprationId { get; set; }

        [XmlElement("cost_center")]
        public string CostCenter { get; set; }

        [XmlElement("cost_center_code")]
        public string CostCenterCode { get; set; }

        [XmlElement("cost_ou")]
        public string CostOu { get; set; }

        [XmlElement("extra")]
        public string Extra { get; set; }

        [XmlElement("form_no")]
        public string FormNo { get; set; }

        [XmlElement("form_status")]
        public string FormStatus { get; set; }

        [XmlElement("receipts_status")]
        public string ReceiptsStatus { get; set; }

        [XmlElement("trip_person_name")]
        public string TripPersonName { get; set; }

        [XmlElement("trip_person_no")]
        public string TripPersonNo { get; set; }

        [XmlElement("work_space")]
        public string WorkSpace { get; set; }
    }
}

