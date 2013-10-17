namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TradeAmount : TopObject
    {
        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        [XmlElement("buyer_cod_fee")]
        public string BuyerCodFee { get; set; }

        [XmlElement("buyer_obtain_point_fee")]
        public long BuyerObtainPointFee { get; set; }

        [XmlElement("cod_fee")]
        public string CodFee { get; set; }

        [XmlElement("commission_fee")]
        public string CommissionFee { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("express_agency_fee")]
        public string ExpressAgencyFee { get; set; }

        [XmlArray("order_amounts"), XmlArrayItem("order_amount")]
        public List<OrderAmount> OrderAmounts { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        [XmlElement("post_fee")]
        public string PostFee { get; set; }

        [XmlArrayItem("promotion_detail"), XmlArray("promotion_details")]
        public List<PromotionDetail> PromotionDetails { get; set; }

        [XmlElement("seller_cod_fee")]
        public string SellerCodFee { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }
    }
}

