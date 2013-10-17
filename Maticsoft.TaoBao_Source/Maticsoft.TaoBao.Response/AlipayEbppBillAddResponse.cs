namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlipayEbppBillAddResponse : TopResponse
    {
        [XmlElement("alipay_order_no")]
        public string AlipayOrderNo { get; set; }

        [XmlElement("bill_date")]
        public string BillDate { get; set; }

        [XmlElement("bill_key")]
        public string BillKey { get; set; }

        [XmlElement("charge_inst")]
        public string ChargeInst { get; set; }

        [XmlElement("charge_inst_name")]
        public string ChargeInstName { get; set; }

        [XmlElement("merchant_order_no")]
        public string MerchantOrderNo { get; set; }

        [XmlElement("order_type")]
        public string OrderType { get; set; }

        [XmlElement("owner_name")]
        public string OwnerName { get; set; }

        [XmlElement("pay_amount")]
        public string PayAmount { get; set; }

        [XmlElement("service_amount")]
        public string ServiceAmount { get; set; }

        [XmlElement("sub_order_type")]
        public string SubOrderType { get; set; }
    }
}

