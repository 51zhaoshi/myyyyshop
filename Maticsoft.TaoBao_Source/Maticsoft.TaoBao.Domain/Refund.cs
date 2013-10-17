namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Refund : TopObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("advance_status")]
        public long AdvanceStatus { get; set; }

        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("company_name")]
        public string CompanyName { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("cs_status")]
        public long CsStatus { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("good_return_time")]
        public string GoodReturnTime { get; set; }

        [XmlElement("good_status")]
        public string GoodStatus { get; set; }

        [XmlElement("has_good_return")]
        public bool HasGoodReturn { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num")]
        public long Num { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("order_status")]
        public string OrderStatus { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("reason")]
        public string Reason { get; set; }

        [XmlElement("refund_fee")]
        public string RefundFee { get; set; }

        [XmlElement("refund_id")]
        public long RefundId { get; set; }

        [XmlElement("refund_remind_timeout")]
        public Maticsoft.TaoBao.Domain.RefundRemindTimeout RefundRemindTimeout { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("shipping_type")]
        public string ShippingType { get; set; }

        [XmlElement("sid")]
        public string Sid { get; set; }

        [XmlElement("split_seller_fee")]
        public string SplitSellerFee { get; set; }

        [XmlElement("split_taobao_fee")]
        public string SplitTaobaoFee { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }
    }
}

