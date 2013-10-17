namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CategoryrecommendItemsGetRequest : ITopRequest<CategoryrecommendItemsGetResponse>
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
            return "taobao.categoryrecommend.items.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("category_id", this.CategoryId);
            dictionary.Add("count", this.Count);
            dictionary.Add("ext", this.Ext);
            dictionary.Add("recommend_type", this.RecommendType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("category_id", this.CategoryId);
            RequestValidator.ValidateRequired("count", this.Count);
            RequestValidator.ValidateRequired("recommend_type", this.RecommendType);
        }

        public long? CategoryId { get; set; }

        public long? Count { get; set; }

        public string Ext { get; set; }

        public long? RecommendType { get; set; }
    }
}

