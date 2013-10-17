namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AlipayEbppBillPayRequest : ITopRequest<AlipayEbppBillPayResponse>
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
            return "alipay.ebpp.bill.pay";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("alipay_order_no", this.AlipayOrderNo);
            dictionary.Add("auth_token", this.AuthToken);
            dictionary.Add("merchant_order_no", this.MerchantOrderNo);
            dictionary.Add("order_type", this.OrderType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("alipay_order_no", this.AlipayOrderNo);
            RequestValidator.ValidateMaxLength("alipay_order_no", this.AlipayOrderNo, 0x1c);
            RequestValidator.ValidateRequired("merchant_order_no", this.MerchantOrderNo);
            RequestValidator.ValidateMaxLength("merchant_order_no", this.MerchantOrderNo, 0x20);
            RequestValidator.ValidateRequired("order_type", this.OrderType);
            RequestValidator.ValidateMaxLength("order_type", this.OrderType, 10);
        }

        public string AlipayOrderNo { get; set; }

        public string AuthToken { get; set; }

        public string MerchantOrderNo { get; set; }

        public string OrderType { get; set; }
    }
}

