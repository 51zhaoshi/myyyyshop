namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoProductsGetRequest : ITopRequest<FenxiaoProductsGetResponse>
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
            return "taobao.fenxiao.products.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_modified", this.EndModified);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("is_authz", this.IsAuthz);
            dictionary.Add("item_ids", this.ItemIds);
            dictionary.Add("outer_id", this.OuterId);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("pids", this.Pids);
            dictionary.Add("productcat_id", this.ProductcatId);
            dictionary.Add("sku_number", this.SkuNumber);
            dictionary.Add("start_modified", this.StartModified);
            dictionary.Add("status", this.Status);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxListSize("item_ids", this.ItemIds, 20);
            RequestValidator.ValidateMaxListSize("pids", this.Pids, 30);
        }

        public DateTime? EndModified { get; set; }

        public string Fields { get; set; }

        public string IsAuthz { get; set; }

        public string ItemIds { get; set; }

        public string OuterId { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string Pids { get; set; }

        public long? ProductcatId { get; set; }

        public string SkuNumber { get; set; }

        public DateTime? StartModified { get; set; }

        public string Status { get; set; }
    }
}

