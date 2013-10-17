namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Trade : TopObject
    {
        [XmlElement("adjust_fee")]
        public string AdjustFee { get; set; }

        [XmlElement("alipay_id")]
        public long AlipayId { get; set; }

        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        [XmlElement("alipay_url")]
        public string AlipayUrl { get; set; }

        [XmlElement("alipay_warn_msg")]
        public string AlipayWarnMsg { get; set; }

        [XmlElement("area_id")]
        public string AreaId { get; set; }

        [XmlElement("available_confirm_fee")]
        public string AvailableConfirmFee { get; set; }

        [XmlElement("buyer_alipay_no")]
        public string BuyerAlipayNo { get; set; }

        [XmlElement("buyer_area")]
        public string BuyerArea { get; set; }

        [XmlElement("buyer_cod_fee")]
        public string BuyerCodFee { get; set; }

        [XmlElement("buyer_email")]
        public string BuyerEmail { get; set; }

        [XmlElement("buyer_flag")]
        public long BuyerFlag { get; set; }

        [XmlElement("buyer_memo")]
        public string BuyerMemo { get; set; }

        [XmlElement("buyer_message")]
        public string BuyerMessage { get; set; }

        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("buyer_obtain_point_fee")]
        public long BuyerObtainPointFee { get; set; }

        [XmlElement("buyer_rate")]
        public bool BuyerRate { get; set; }

        [XmlElement("can_rate")]
        public bool CanRate { get; set; }

        [XmlElement("cod_fee")]
        public string CodFee { get; set; }

        [XmlElement("cod_status")]
        public string CodStatus { get; set; }

        [XmlElement("commission_fee")]
        public string CommissionFee { get; set; }

        [XmlElement("consign_time")]
        public string ConsignTime { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("credit_card_fee")]
        public string CreditCardFee { get; set; }

        [XmlElement("discount_fee")]
        public string DiscountFee { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("eticket_ext")]
        public string EticketExt { get; set; }

        [XmlElement("express_agency_fee")]
        public string ExpressAgencyFee { get; set; }

        [XmlElement("has_buyer_message")]
        public bool HasBuyerMessage { get; set; }

        [XmlElement("has_post_fee")]
        public bool HasPostFee { get; set; }

        [XmlElement("has_yfx")]
        public bool HasYfx { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("invoice_name")]
        public string InvoiceName { get; set; }

        [XmlElement("is_3D")]
        public bool Is3D { get; set; }

        [XmlElement("is_brand_sale")]
        public bool IsBrandSale { get; set; }

        [XmlElement("is_force_wlb")]
        public bool IsForceWlb { get; set; }

        [XmlElement("is_lgtype")]
        public bool IsLgtype { get; set; }

        [XmlElement("lg_aging")]
        public string LgAging { get; set; }

        [XmlElement("lg_aging_type")]
        public string LgAgingType { get; set; }

        [XmlElement("mark_desc")]
        public string MarkDesc { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num")]
        public long Num { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("nut_feature")]
        public string NutFeature { get; set; }

        [XmlArrayItem("order"), XmlArray("orders")]
        public List<Order> Orders { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        [XmlElement("point_fee")]
        public long PointFee { get; set; }

        [XmlElement("post_fee")]
        public string PostFee { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("promotion")]
        public string Promotion { get; set; }

        [XmlArrayItem("promotion_detail"), XmlArray("promotion_details")]
        public List<PromotionDetail> PromotionDetails { get; set; }

        [XmlElement("real_point_fee")]
        public long RealPointFee { get; set; }

        [XmlElement("received_payment")]
        public string ReceivedPayment { get; set; }

        [XmlElement("receiver_address")]
        public string ReceiverAddress { get; set; }

        [XmlElement("receiver_city")]
        public string ReceiverCity { get; set; }

        [XmlElement("receiver_district")]
        public string ReceiverDistrict { get; set; }

        [XmlElement("receiver_mobile")]
        public string ReceiverMobile { get; set; }

        [XmlElement("receiver_name")]
        public string ReceiverName { get; set; }

        [XmlElement("receiver_phone")]
        public string ReceiverPhone { get; set; }

        [XmlElement("receiver_state")]
        public string ReceiverState { get; set; }

        [XmlElement("receiver_zip")]
        public string ReceiverZip { get; set; }

        [XmlElement("seller_alipay_no")]
        public string SellerAlipayNo { get; set; }

        [XmlElement("seller_cod_fee")]
        public string SellerCodFee { get; set; }

        [XmlElement("seller_email")]
        public string SellerEmail { get; set; }

        [XmlElement("seller_flag")]
        public long SellerFlag { get; set; }

        [XmlElement("seller_memo")]
        public string SellerMemo { get; set; }

        [XmlElement("seller_mobile")]
        public string SellerMobile { get; set; }

        [XmlElement("seller_name")]
        public string SellerName { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("seller_phone")]
        public string SellerPhone { get; set; }

        [XmlElement("seller_rate")]
        public bool SellerRate { get; set; }

        [XmlArrayItem("service_order"), XmlArray("service_orders")]
        public List<ServiceOrder> ServiceOrders { get; set; }

        [XmlElement("shipping_type")]
        public string ShippingType { get; set; }

        [XmlElement("snapshot")]
        public string Snapshot { get; set; }

        [XmlElement("snapshot_url")]
        public string SnapshotUrl { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("step_paid_fee")]
        public string StepPaidFee { get; set; }

        [XmlElement("step_trade_status")]
        public string StepTradeStatus { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("timeout_action_time")]
        public string TimeoutActionTime { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }

        [XmlElement("trade_from")]
        public string TradeFrom { get; set; }

        [XmlElement("trade_memo")]
        public string TradeMemo { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("yfx_fee")]
        public string YfxFee { get; set; }

        [XmlElement("yfx_id")]
        public string YfxId { get; set; }
    }
}

