namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Discount : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlArrayItem("discount_detail"), XmlArray("details")]
        public List<DiscountDetail> Details { get; set; }

        [XmlElement("discount_id")]
        public long DiscountId { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}

