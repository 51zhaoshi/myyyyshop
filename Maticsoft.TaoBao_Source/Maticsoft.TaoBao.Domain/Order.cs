namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Order : TopObject
    {
        [XmlElement("adjust_fee")]
        public string AdjustFee { get; set; }

        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("buyer_rate")]
        public bool BuyerRate { get; set; }

        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("discount_fee")]
        public string DiscountFee { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("is_oversold")]
        public bool IsOversold { get; set; }

        [XmlElement("is_service_order")]
        public bool IsServiceOrder { get; set; }

        [XmlElement("item_meal_id")]
        public long ItemMealId { get; set; }

        [XmlElement("item_meal_name")]
        public string ItemMealName { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num")]
        public long Num { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("order_from")]
        public string OrderFrom { get; set; }

        [XmlElement("outer_iid")]
        public string OuterIid { get; set; }

        [XmlElement("outer_sku_id")]
        public string OuterSkuId { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("refund_id")]
        public long RefundId { get; set; }

        [XmlElement("refund_status")]
        public string RefundStatus { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("seller_rate")]
        public bool SellerRate { get; set; }

        [XmlElement("seller_type")]
        public string SellerType { get; set; }

        [XmlElement("sku_id")]
        public string SkuId { get; set; }

        [XmlElement("sku_properties_name")]
        public string SkuPropertiesName { get; set; }

        [XmlElement("snapshot")]
        public string Snapshot { get; set; }

        [XmlElement("snapshot_url")]
        public string SnapshotUrl { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("timeout_action_time")]
        public string TimeoutActionTime { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }
    }
}

