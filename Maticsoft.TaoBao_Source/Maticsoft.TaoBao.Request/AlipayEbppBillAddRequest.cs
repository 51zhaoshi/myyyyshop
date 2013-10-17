namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AlipayEbppBillAddRequest : ITopRequest<AlipayEbppBillAddResponse>
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
            return "alipay.ebpp.bill.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("auth_token", this.AuthToken);
            dictionary.Add("bill_date", this.BillDate);
            dictionary.Add("bill_key", this.BillKey);
            dictionary.Add("charge_inst", this.ChargeInst);
            dictionary.Add("merchant_order_no", this.MerchantOrderNo);
            dictionary.Add("mobile", this.Mobile);
            dictionary.Add("order_type", this.OrderType);
            dictionary.Add("owner_name", this.OwnerName);
            dictionary.Add("pay_amount", this.PayAmount);
            dictionary.Add("service_amount", this.ServiceAmount);
            dictionary.Add("sub_order_type", this.SubOrderType);
            dictionary.Add("traffic_location", this.TrafficLocation);
            dictionary.Add("traffic_regulations", this.TrafficRegulations);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("bill_key", this.BillKey);
            RequestValidator.ValidateMaxLength("bill_key", this.BillKey, 50);
            RequestValidator.ValidateRequired("charge_inst", this.ChargeInst);
            RequestValidator.ValidateMaxLength("charge_inst", this.ChargeInst, 80);
            RequestValidator.ValidateRequired("merchant_order_no", this.MerchantOrderNo);
            RequestValidator.ValidateMaxLength("merchant_order_no", this.MerchantOrderNo, 0x20);
            RequestValidator.ValidateRequired("mobile", this.Mobile);
            RequestValidator.ValidateRequired("order_type", this.OrderType);
            RequestValidator.ValidateMaxLength("order_type", this.OrderType, 10);
            RequestValidator.ValidateMaxLength("owner_name", this.OwnerName, 50);
            RequestValidator.ValidateRequired("pay_amount", this.PayAmount);
            RequestValidator.ValidateRequired("sub_order_type", this.SubOrderType);
            RequestValidator.ValidateMaxLength("sub_order_type", this.SubOrderType, 10);
        }

        public string AuthToken { get; set; }

        public string BillDate { get; set; }

        public string BillKey { get; set; }

        public string ChargeInst { get; set; }

        public string MerchantOrderNo { get; set; }

        public string Mobile { get; set; }

        public string OrderType { get; set; }

        public string OwnerName { get; set; }

        public string PayAmount { get; set; }

        public string ServiceAmount { get; set; }

        public string SubOrderType { get; set; }

        public string TrafficLocation { get; set; }

        public string TrafficRegulations { get; set; }
    }
}

