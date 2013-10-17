namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AlipayUserTradeSearchRequest : ITopRequest<AlipayUserTradeSearchResponse>
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
            return "alipay.user.trade.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("alipay_order_no", this.AlipayOrderNo);
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("merchant_order_no", this.MerchantOrderNo);
            dictionary.Add("order_from", this.OrderFrom);
            dictionary.Add("order_status", this.OrderStatus);
            dictionary.Add("order_type", this.OrderType);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("start_time", this.StartTime);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("end_time", this.EndTime);
            RequestValidator.ValidateRequired("page_size", this.PageSize);
            RequestValidator.ValidateRequired("start_time", this.StartTime);
        }

        public string AlipayOrderNo { get; set; }

        public string EndTime { get; set; }

        public string MerchantOrderNo { get; set; }

        public string OrderFrom { get; set; }

        public string OrderStatus { get; set; }

        public string OrderType { get; set; }

        public string PageNo { get; set; }

        public string PageSize { get; set; }

        public string StartTime { get; set; }
    }
}

