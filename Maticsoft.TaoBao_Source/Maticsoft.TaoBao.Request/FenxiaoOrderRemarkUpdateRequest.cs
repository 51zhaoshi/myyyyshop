namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoOrderRemarkUpdateRequest : ITopRequest<FenxiaoOrderRemarkUpdateResponse>
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
            return "taobao.fenxiao.order.remark.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("purchase_order_id", this.PurchaseOrderId);
            dictionary.Add("supplier_memo", this.SupplierMemo);
            dictionary.Add("supplier_memo_flag", this.SupplierMemoFlag);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("purchase_order_id", this.PurchaseOrderId);
            RequestValidator.ValidateRequired("supplier_memo", this.SupplierMemo);
            RequestValidator.ValidateMaxValue("supplier_memo_flag", this.SupplierMemoFlag, 5L);
            RequestValidator.ValidateMinValue("supplier_memo_flag", this.SupplierMemoFlag, 1L);
        }

        public long? PurchaseOrderId { get; set; }

        public string SupplierMemo { get; set; }

        public long? SupplierMemoFlag { get; set; }
    }
}

