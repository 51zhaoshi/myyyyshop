namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TaobaokeReportMember : TopObject
    {
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        [XmlElement("category_id")]
        public long CategoryId { get; set; }

        [XmlElement("category_name")]
        public string CategoryName { get; set; }

        [XmlElement("commission")]
        public string Commission { get; set; }

        [XmlElement("commission_rate")]
        public string CommissionRate { get; set; }

        [XmlElement("confirm_time")]
        public string ConfirmTime { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("item_num")]
        public long ItemNum { get; set; }

        [XmlElement("item_title")]
        public string ItemTitle { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("outer_code")]
        public string OuterCode { get; set; }

        [XmlElement("pay_price")]
        public string PayPrice { get; set; }

        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        [XmlElement("real_pay_fee")]
        public string RealPayFee { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("shop_title")]
        public string ShopTitle { get; set; }

        [XmlElement("trade_id")]
        public long TradeId { get; set; }

        [XmlElement("trade_parent_id")]
        public long TradeParentId { get; set; }
    }
}

