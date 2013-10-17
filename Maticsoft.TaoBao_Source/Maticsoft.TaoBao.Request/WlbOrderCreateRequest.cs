namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbOrderCreateRequest : ITopRequest<WlbOrderCreateResponse>
    {
        private IDictionary<string, string> otherParameters;

        public void AddOtherParameter(string key, string value)
        {
            if (this.otherParameters == null)
            {
                this.otherParameters = new TopDictionary();
            }
            this.otherParameters.Add(key, value);
        }

        public string GetApiName()
        {
            return "taobao.wlb.order.create";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("alipay_no", this.AlipayNo);
            dictionary.Add("attributes", this.Attributes);
            dictionary.Add("buyer_nick", this.BuyerNick);
            dictionary.Add("expect_end_time", this.ExpectEndTime);
            dictionary.Add("expect_start_time", this.ExpectStartTime);
            dictionary.Add("invoince_info", this.InvoinceInfo);
            dictionary.Add("is_finished", this.IsFinished);
            dictionary.Add("order_code", this.OrderCode);
            dictionary.Add("order_flag", this.OrderFlag);
            dictionary.Add("order_item_list", this.OrderItemList);
            dictionary.Add("order_sub_type", this.OrderSubType);
            dictionary.Add("order_type", this.OrderType);
            dictionary.Add("out_biz_code", this.OutBizCode);
            dictionary.Add("package_count", this.PackageCount);
            dictionary.Add("payable_amount", this.PayableAmount);
            dictionary.Add("prev_order_code", this.PrevOrderCode);
            dictionary.Add("receiver_info", this.ReceiverInfo);
            dictionary.Add("remark", this.Remark);
            dictionary.Add("schedule_end", this.ScheduleEnd);
            dictionary.Add("schedule_start", this.ScheduleStart);
            dictionary.Add("schedule_type", this.ScheduleType);
            dictionary.Add("sender_info", this.SenderInfo);
            dictionary.Add("service_fee", this.ServiceFee);
            dictionary.Add("store_code", this.StoreCode);
            dictionary.Add("tms_info", this.TmsInfo);
            dictionary.Add("tms_order_code", this.TmsOrderCode);
            dictionary.Add("tms_service_code", this.TmsServiceCode);
            dictionary.Add("total_amount", this.TotalAmount);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("buyer_nick", this.BuyerNick, 0x40);
            RequestValidator.ValidateRequired("is_finished", this.IsFinished);
            RequestValidator.ValidateRequired("order_item_list", this.OrderItemList);
            RequestValidator.ValidateRequired("order_sub_type", this.OrderSubType);
            RequestValidator.ValidateRequired("order_type", this.OrderType);
            RequestValidator.ValidateRequired("out_biz_code", this.OutBizCode);
            RequestValidator.ValidateMaxLength("out_biz_code", this.OutBizCode, 0x80);
            RequestValidator.ValidateMaxLength("remark", this.Remark, 0xfa0);
            RequestValidator.ValidateRequired("store_code", this.StoreCode);
            RequestValidator.ValidateMaxLength("store_code", this.StoreCode, 0x40);
            RequestValidator.ValidateMaxLength("tms_service_code", this.TmsServiceCode, 0x40);
        }

        public string AlipayNo { get; set; }

        public string Attributes { get; set; }

        public string BuyerNick { get; set; }

        public DateTime? ExpectEndTime { get; set; }

        public DateTime? ExpectStartTime { get; set; }

        public string InvoinceInfo { get; set; }

        public bool? IsFinished { get; set; }

        public string OrderCode { get; set; }

        public string OrderFlag { get; set; }

        public string OrderItemList { get; set; }

        public string OrderSubType { get; set; }

        public string OrderType { get; set; }

        public string OutBizCode { get; set; }

        public long? PackageCount { get; set; }

        public long? PayableAmount { get; set; }

        public string PrevOrderCode { get; set; }

        public string ReceiverInfo { get; set; }

        public string Remark { get; set; }

        public string ScheduleEnd { get; set; }

        public string ScheduleStart { get; set; }

        public string ScheduleType { get; set; }

        public string SenderInfo { get; set; }

        public long? ServiceFee { get; set; }

        public string StoreCode { get; set; }

        public string TmsInfo { get; set; }

        public string TmsOrderCode { get; set; }

        public string TmsServiceCode { get; set; }

        public long? TotalAmount { get; set; }
    }
}

