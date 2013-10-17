namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class BaseInfo : TopObject
    {
        [XmlElement("account_no")]
        public string AccountNo { get; set; }

        [XmlElement("alipay_trade_no")]
        public string AlipayTradeNo { get; set; }

        [XmlElement("book_way")]
        public long BookWay { get; set; }

        [XmlElement("commission")]
        public string Commission { get; set; }

        [XmlElement("commission_discount")]
        public string CommissionDiscount { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("extra")]
        public string Extra { get; set; }

        [XmlElement("modify_time")]
        public string ModifyTime { get; set; }

        [XmlElement("order_id")]
        public long OrderId { get; set; }

        [XmlElement("pay_latest_time")]
        public string PayLatestTime { get; set; }

        [XmlElement("pay_status")]
        public long PayStatus { get; set; }

        [XmlElement("relation_email")]
        public string RelationEmail { get; set; }

        [XmlElement("relation_mobile")]
        public string RelationMobile { get; set; }

        [XmlElement("relation_name")]
        public string RelationName { get; set; }

        [XmlElement("relation_phone_bak")]
        public string RelationPhoneBak { get; set; }

        [XmlElement("relative_order_id")]
        public long RelativeOrderId { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }

        [XmlElement("total_price")]
        public long TotalPrice { get; set; }

        [XmlElement("trip_type")]
        public long TripType { get; set; }
    }
}

