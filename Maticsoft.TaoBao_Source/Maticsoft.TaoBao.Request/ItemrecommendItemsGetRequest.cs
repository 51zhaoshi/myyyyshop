namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemrecommendItemsGetRequest : ITopRequest<ItemrecommendItemsGetResponse>
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
            return "taobao.itemrecommend.items.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("count", this.Count);
            dictionary.Add("ext", this.Ext);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("recommend_type", this.RecommendType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("count", this.Count);
            RequestValidator.ValidateRequired("item_id", this.ItemId);
            RequestValidator.ValidateRequired("recommend_type", this.RecommendType);
        }

        public long? Count { get; set; }

        public string Ext { get; set; }

        public long? ItemId { get; set; }

        public long? RecommendType { get; set; }
    }
}

