namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoDistributorItemsGetRequest : ITopRequest<FenxiaoDistributorItemsGetResponse>
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
            return "taobao.fenxiao.distributor.items.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("distributor_id", this.DistributorId);
            dictionary.Add("end_modified", this.EndModified);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("start_modified", this.StartModified);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public long? DistributorId { get; set; }

        public DateTime? EndModified { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public long? ProductId { get; set; }

        public DateTime? StartModified { get; set; }
    }
}

