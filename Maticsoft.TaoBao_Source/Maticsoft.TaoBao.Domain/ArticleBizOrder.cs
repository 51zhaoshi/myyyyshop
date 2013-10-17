namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ArticleBizOrder : TopObject
    {
        [XmlElement("article_code")]
        public string ArticleCode { get; set; }

        [XmlElement("article_name")]
        public string ArticleName { get; set; }

        [XmlElement("biz_order_id")]
        public long BizOrderId { get; set; }

        [XmlElement("biz_type")]
        public long BizType { get; set; }

        [XmlElement("create")]
        public string Create { get; set; }

        [XmlElement("fee")]
        public string Fee { get; set; }

        [XmlElement("item_code")]
        public string ItemCode { get; set; }

        [XmlElement("item_name")]
        public string ItemName { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("order_cycle")]
        public string OrderCycle { get; set; }

        [XmlElement("order_cycle_end")]
        public string OrderCycleEnd { get; set; }

        [XmlElement("order_cycle_start")]
        public string OrderCycleStart { get; set; }

        [XmlElement("order_id")]
        public long OrderId { get; set; }

        [XmlElement("prom_fee")]
        public string PromFee { get; set; }

        [XmlElement("refund_fee")]
        public string RefundFee { get; set; }

        [XmlElement("total_pay_fee")]
        public string TotalPayFee { get; set; }
    }
}

