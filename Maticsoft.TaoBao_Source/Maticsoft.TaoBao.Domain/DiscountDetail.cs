namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class DiscountDetail : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("detail_id")]
        public long DetailId { get; set; }

        [XmlElement("discount_type")]
        public string DiscountType { get; set; }

        [XmlElement("discount_value")]
        public long DiscountValue { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("target_id")]
        public long TargetId { get; set; }

        [XmlElement("target_name")]
        public string TargetName { get; set; }

        [XmlElement("target_type")]
        public string TargetType { get; set; }
    }
}

