namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TradeRecord : TopObject
    {
        [XmlElement("alipay_order_no")]
        public string AlipayOrderNo { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("in_out_type")]
        public string InOutType { get; set; }

        [XmlElement("merchant_order_no")]
        public string MerchantOrderNo { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("opposite_logon_id")]
        public string OppositeLogonId { get; set; }

        [XmlElement("opposite_name")]
        public string OppositeName { get; set; }

        [XmlElement("opposite_user_id")]
        public string OppositeUserId { get; set; }

        [XmlElement("order_from")]
        public string OrderFrom { get; set; }

        [XmlElement("order_status")]
        public string OrderStatus { get; set; }

        [XmlElement("order_title")]
        public string OrderTitle { get; set; }

        [XmlElement("order_type")]
        public string OrderType { get; set; }

        [XmlElement("owner_logon_id")]
        public string OwnerLogonId { get; set; }

        [XmlElement("owner_name")]
        public string OwnerName { get; set; }

        [XmlElement("owner_user_id")]
        public string OwnerUserId { get; set; }

        [XmlElement("service_charge")]
        public string ServiceCharge { get; set; }

        [XmlElement("total_amount")]
        public string TotalAmount { get; set; }
    }
}

