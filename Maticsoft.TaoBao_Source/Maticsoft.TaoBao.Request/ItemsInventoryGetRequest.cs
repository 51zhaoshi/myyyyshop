namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemsInventoryGetRequest : ITopRequest<ItemsInventoryGetResponse>
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
            return "taobao.items.inventory.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("banner", this.Banner);
            dictionary.Add("cid", this.Cid);
            dictionary.Add("end_modified", this.EndModified);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("has_discount", this.HasDiscount);
            dictionary.Add("is_ex", this.IsEx);
            dictionary.Add("is_taobao", this.IsTaobao);
            dictionary.Add("order_by", this.OrderBy);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("q", this.Q);
            dictionary.Add("seller_cids", this.SellerCids);
            dictionary.Add("start_modified", this.StartModified);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMinValue("cid", this.Cid, 0L);
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxListSize("seller_cids", this.SellerCids, 0x20);
        }

        public string Banner { get; set; }

        public long? Cid { get; set; }

        public DateTime? EndModified { get; set; }

        public string Fields { get; set; }

        public bool? HasDiscount { get; set; }

        public bool? IsEx { get; set; }

        public bool? IsTaobao { get; set; }

        public string OrderBy { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string Q { get; set; }

        public string SellerCids { get; set; }

        public DateTime? StartModified { get; set; }
    }
}

