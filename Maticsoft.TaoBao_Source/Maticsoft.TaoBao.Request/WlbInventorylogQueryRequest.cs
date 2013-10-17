namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbInventorylogQueryRequest : ITopRequest<WlbInventorylogQueryResponse>
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
            return "taobao.wlb.inventorylog.query";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("gmt_end", this.GmtEnd);
            dictionary.Add("gmt_start", this.GmtStart);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("op_type", this.OpType);
            dictionary.Add("op_user_id", this.OpUserId);
            dictionary.Add("order_code", this.OrderCode);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("store_code", this.StoreCode);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public DateTime? GmtEnd { get; set; }

        public DateTime? GmtStart { get; set; }

        public long? ItemId { get; set; }

        public string OpType { get; set; }

        public long? OpUserId { get; set; }

        public string OrderCode { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string StoreCode { get; set; }
    }
}

