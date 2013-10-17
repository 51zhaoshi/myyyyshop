namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlipayEbppBillPayResponse : TopResponse
    {
        [XmlElement("alipay_order_no")]
        public string AlipayOrderNo { get; set; }

        [XmlElement("merchant_order_no")]
        public string MerchantOrderNo { get; set; }

        [XmlElement("order_type")]
        public string OrderType { get; set; }
    }
}

