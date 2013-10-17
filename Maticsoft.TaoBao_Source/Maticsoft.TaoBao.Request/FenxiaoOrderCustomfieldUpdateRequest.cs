namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoOrderCustomfieldUpdateRequest : ITopRequest<FenxiaoOrderCustomfieldUpdateResponse>
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
            return "taobao.fenxiao.order.customfield.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("isv_custom_key", this.IsvCustomKey);
            dictionary.Add("isv_custom_value", this.IsvCustomValue);
            dictionary.Add("purchase_order_id", this.PurchaseOrderId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("isv_custom_key", this.IsvCustomKey);
            RequestValidator.ValidateRequired("purchase_order_id", this.PurchaseOrderId);
        }

        public string IsvCustomKey { get; set; }

        public string IsvCustomValue { get; set; }

        public long? PurchaseOrderId { get; set; }
    }
}

