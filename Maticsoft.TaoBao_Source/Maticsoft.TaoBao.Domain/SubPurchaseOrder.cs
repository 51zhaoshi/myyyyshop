namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SubPurchaseOrder : TopObject
    {
        [XmlElement("auction_price")]
        public string AuctionPrice { get; set; }

        [XmlElement("buyer_payment")]
        public string BuyerPayment { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("distributor_payment")]
        public string DistributorPayment { get; set; }

        [XmlElement("fenxiao_id")]
        public long FenxiaoId { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("item_outer_id")]
        public string ItemOuterId { get; set; }

        [XmlElement("num")]
        public long Num { get; set; }

        [XmlElement("old_sku_properties")]
        public string OldSkuProperties { get; set; }

        [XmlElement("order_200_status")]
        public string Order200Status { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("refund_fee")]
        public string RefundFee { get; set; }

        [XmlElement("sku_id")]
        public long SkuId { get; set; }

        [XmlElement("sku_outer_id")]
        public string SkuOuterId { get; set; }

        [XmlElement("sku_properties")]
        public string SkuProperties { get; set; }

        [XmlElement("snapshot_url")]
        public string SnapshotUrl { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tc_order_id")]
        public long TcOrderId { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }
    }
}

