namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemQueryRequest : ITopRequest<WlbItemQueryResponse>
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
            return "taobao.wlb.item.query";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("is_sku", this.IsSku);
            dictionary.Add("item_code", this.ItemCode);
            dictionary.Add("item_type", this.ItemType);
            dictionary.Add("name", this.Name);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("parent_id", this.ParentId);
            dictionary.Add("status", this.Status);
            dictionary.Add("title", this.Title);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("item_code", this.ItemCode, 0x40);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1L);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 50L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
            RequestValidator.ValidateMaxLength("title", this.Title, 0xff);
        }

        public string IsSku { get; set; }

        public string ItemCode { get; set; }

        public string ItemType { get; set; }

        public string Name { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public long? ParentId { get; set; }

        public string Status { get; set; }

        public string Title { get; set; }
    }
}

