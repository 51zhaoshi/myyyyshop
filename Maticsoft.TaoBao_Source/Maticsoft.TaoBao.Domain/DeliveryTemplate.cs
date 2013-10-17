namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class DeliveryTemplate : TopObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("assumer")]
        public long Assumer { get; set; }

        [XmlElement("consign_area_id")]
        public long ConsignAreaId { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlArray("fee_list"), XmlArrayItem("top_fee")]
        public List<TopFee> FeeList { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("supports")]
        public string Supports { get; set; }

        [XmlElement("template_id")]
        public long TemplateId { get; set; }

        [XmlElement("valuation")]
        public long Valuation { get; set; }
    }
}

