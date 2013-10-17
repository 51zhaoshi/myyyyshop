namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FavoriteSearchRequest : ITopRequest<FavoriteSearchResponse>
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
            return "taobao.favorite.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("collect_type", this.CollectType);
            dictionary.Add("page_no", this.PageNo);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("collect_type", this.CollectType);
            RequestValidator.ValidateMaxLength("collect_type", this.CollectType, 4);
            RequestValidator.ValidateRequired("page_no", this.PageNo);
            RequestValidator.ValidateMaxValue("page_no", this.PageNo, 100L);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1L);
        }

        public string CollectType { get; set; }

        public long? PageNo { get; set; }
    }
}

