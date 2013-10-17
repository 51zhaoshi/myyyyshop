namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Cooperation : TopObject
    {
        [XmlArray("auth_payway"), XmlArrayItem("string")]
        public List<string> AuthPayway { get; set; }

        [XmlElement("cooperate_id")]
        public long CooperateId { get; set; }

        [XmlElement("distributor_id")]
        public long DistributorId { get; set; }

        [XmlElement("distributor_nick")]
        public string DistributorNick { get; set; }

        [XmlElement("end_date")]
        public string EndDate { get; set; }

        [XmlElement("grade_id")]
        public long GradeId { get; set; }

        [XmlElement("product_line")]
        public string ProductLine { get; set; }

        [XmlElement("start_date")]
        public string StartDate { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("supplier_id")]
        public long SupplierId { get; set; }

        [XmlElement("supplier_nick")]
        public string SupplierNick { get; set; }

        [XmlElement("trade_type")]
        public string TradeType { get; set; }
    }
}

