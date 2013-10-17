namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoOrderMessageAddRequest : ITopRequest<FenxiaoOrderMessageAddResponse>
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
            return "taobao.fenxiao.order.message.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("message", this.Message);
            dictionary.Add("purchase_order_id", this.PurchaseOrderId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("message", this.Message);
            RequestValidator.ValidateRequired("purchase_order_id", this.PurchaseOrderId);
        }

        public string Message { get; set; }

        public long? PurchaseOrderId { get; set; }
    }
}

