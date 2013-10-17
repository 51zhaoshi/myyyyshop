namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbOrder : TopObject
    {
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("cancel_order_status")]
        public long CancelOrderStatus { get; set; }

        [XmlElement("confirm_status")]
        public string ConfirmStatus { get; set; }

        [XmlElement("expect_end_time")]
        public string ExpectEndTime { get; set; }

        [XmlElement("expect_start_time")]
        public string ExpectStartTime { get; set; }

        [XmlElement("invoice_info")]
        public string InvoiceInfo { get; set; }

        [XmlElement("item_kinds_count")]
        public long ItemKindsCount { get; set; }

        [XmlElement("operate_type")]
        public string OperateType { get; set; }

        [XmlElement("order_code")]
        public string OrderCode { get; set; }

        [XmlElement("order_flag")]
        public long OrderFlag { get; set; }

        [XmlElement("order_source")]
        public string OrderSource { get; set; }

        [XmlElement("order_source_code")]
        public string OrderSourceCode { get; set; }

        [XmlElement("order_status")]
        public string OrderStatus { get; set; }

        [XmlElement("order_status_reason")]
        public string OrderStatusReason { get; set; }

        [XmlElement("order_sub_type")]
        public string OrderSubType { get; set; }

        [XmlElement("order_type")]
        public string OrderType { get; set; }

        [XmlElement("prev_order_code")]
        public string PrevOrderCode { get; set; }

        [XmlElement("real_kinds_count")]
        public long RealKindsCount { get; set; }

        [XmlElement("receivable_amount")]
        public long ReceivableAmount { get; set; }

        [XmlElement("receiver_address")]
        public string ReceiverAddress { get; set; }

        [XmlElement("receiver_area")]
        public string ReceiverArea { get; set; }

        [XmlElement("receiver_city")]
        public string ReceiverCity { get; set; }

        [XmlElement("receiver_mail")]
        public string ReceiverMail { get; set; }

        [XmlElement("receiver_mobile")]
        public string ReceiverMobile { get; set; }

        [XmlElement("receiver_name")]
        public string ReceiverName { get; set; }

        [XmlElement("receiver_phone")]
        public string ReceiverPhone { get; set; }

        [XmlElement("receiver_province")]
        public string ReceiverProvince { get; set; }

        [XmlElement("receiver_wangwang")]
        public string ReceiverWangwang { get; set; }

        [XmlElement("receiver_zip_code")]
        public string ReceiverZipCode { get; set; }

        [XmlElement("remark")]
        public string Remark { get; set; }

        [XmlElement("schedule_day")]
        public string ScheduleDay { get; set; }

        [XmlElement("schedule_end")]
        public string ScheduleEnd { get; set; }

        [XmlElement("schedule_speed")]
        public long ScheduleSpeed { get; set; }

        [XmlElement("schedule_start")]
        public string ScheduleStart { get; set; }

        [XmlElement("sender_address")]
        public string SenderAddress { get; set; }

        [XmlElement("sender_area")]
        public string SenderArea { get; set; }

        [XmlElement("sender_city")]
        public string SenderCity { get; set; }

        [XmlElement("sender_email")]
        public string SenderEmail { get; set; }

        [XmlElement("sender_mobile")]
        public string SenderMobile { get; set; }

        [XmlElement("sender_name")]
        public string SenderName { get; set; }

        [XmlElement("sender_phone")]
        public string SenderPhone { get; set; }

        [XmlElement("sender_province")]
        public string SenderProvince { get; set; }

        [XmlElement("sender_zip_code")]
        public string SenderZipCode { get; set; }

        [XmlElement("service_fee")]
        public long ServiceFee { get; set; }

        [XmlElement("shipping_type")]
        public string ShippingType { get; set; }

        [XmlElement("store_code")]
        public string StoreCode { get; set; }

        [XmlElement("tms_tp_code")]
        public string TmsTpCode { get; set; }

        [XmlElement("total_amount")]
        public long TotalAmount { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }

        [XmlElement("user_nick")]
        public string UserNick { get; set; }
    }
}

