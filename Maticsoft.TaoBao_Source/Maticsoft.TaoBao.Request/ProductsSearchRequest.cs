namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ProductsSearchRequest : ITopRequest<ProductsSearchResponse>
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
            return "taobao.products.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cid", this.Cid);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("props", this.Props);
            dictionary.Add("q", this.Q);
            dictionary.Add("status", this.Status);
            dictionary.Add("vertical_market", this.VerticalMarket);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxLength("status", this.Status, 20);
            RequestValidator.ValidateMinValue("vertical_market", this.VerticalMarket, 0L);
        }

        public long? Cid { get; set; }

        public string Fields { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string Props { get; set; }

        public string Q { get; set; }

        public string Status { get; set; }

        public long? VerticalMarket { get; set; }
    }
}

