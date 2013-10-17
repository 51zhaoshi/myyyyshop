namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PurchaseOrder : TopObject
    {
        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("consign_time")]
        public string ConsignTime { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("distributor_from")]
        public string DistributorFrom { get; set; }

        [XmlElement("distributor_payment")]
        public string DistributorPayment { get; set; }

        [XmlElement("distributor_username")]
        public string DistributorUsername { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("fenxiao_id")]
        public long FenxiaoId { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlArray("isv_custom_key"), XmlArrayItem("string")]
        public List<string> IsvCustomKey { get; set; }

        [XmlArrayItem("string"), XmlArray("isv_custom_value")]
        public List<string> IsvCustomValue { get; set; }

        [XmlElement("logistics_company_name")]
        public string LogisticsCompanyName { get; set; }

        [XmlElement("logistics_id")]
        public string LogisticsId { get; set; }

        [XmlElement("memo")]
        public string Memo { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        [XmlElement("pay_type")]
        public string PayType { get; set; }

        [XmlElement("post_fee")]
        public string PostFee { get; set; }

        [XmlElement("receiver")]
        public Maticsoft.TaoBao.Domain.Receiver Receiver { get; set; }

        [XmlElement("shipping")]
        public string Shipping { get; set; }

        [XmlElement("snapshot_url")]
        public string SnapshotUrl { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlArray("sub_purchase_orders"), XmlArrayItem("sub_purchase_order")]
        public List<SubPurchaseOrder> SubPurchaseOrders { get; set; }

        [XmlElement("supplier_flag")]
        public long SupplierFlag { get; set; }

        [XmlElement("supplier_from")]
        public string SupplierFrom { get; set; }

        [XmlElement("supplier_memo")]
        public string SupplierMemo { get; set; }

        [XmlElement("supplier_username")]
        public string SupplierUsername { get; set; }

        [XmlElement("tc_order_id")]
        public long TcOrderId { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }

        [XmlElement("trade_type")]
        public string TradeType { get; set; }
    }
}

