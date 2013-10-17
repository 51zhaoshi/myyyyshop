namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbOutInventoryChangeNotifyRequest : ITopRequest<WlbOutInventoryChangeNotifyResponse>
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
            return "taobao.wlb.out.inventory.change.notify";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("change_count", this.ChangeCount);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("op_type", this.OpType);
            dictionary.Add("order_source_code", this.OrderSourceCode);
            dictionary.Add("out_biz_code", this.OutBizCode);
            dictionary.Add("result_count", this.ResultCount);
            dictionary.Add("source", this.Source);
            dictionary.Add("store_code", this.StoreCode);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("change_count", this.ChangeCount);
            RequestValidator.ValidateRequired("item_id", this.ItemId);
            RequestValidator.ValidateRequired("op_type", this.OpType);
            RequestValidator.ValidateRequired("out_biz_code", this.OutBizCode);
            RequestValidator.ValidateRequired("result_count", this.ResultCount);
            RequestValidator.ValidateRequired("source", this.Source);
            RequestValidator.ValidateRequired("type", this.Type);
        }

        public long? ChangeCount { get; set; }

        public long? ItemId { get; set; }

        public string OpType { get; set; }

        public string OrderSourceCode { get; set; }

        public string OutBizCode { get; set; }

        public long? ResultCount { get; set; }

        public string Source { get; set; }

        public string StoreCode { get; set; }

        public string Type { get; set; }
    }
}

