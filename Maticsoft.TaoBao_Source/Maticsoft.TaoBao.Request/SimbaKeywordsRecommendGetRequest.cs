namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaKeywordsRecommendGetRequest : ITopRequest<SimbaKeywordsRecommendGetResponse>
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
            return "taobao.simba.keywords.recommend.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_id", this.AdgroupId);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("order_by", this.OrderBy);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("pertinence", this.Pertinence);
            dictionary.Add("search", this.Search);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("adgroup_id", this.AdgroupId);
            RequestValidator.ValidateRequired("page_no", this.PageNo);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1L);
            RequestValidator.ValidateRequired("page_size", this.PageSize);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 200L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
        }

        public long? AdgroupId { get; set; }

        public string Nick { get; set; }

        public string OrderBy { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string Pertinence { get; set; }

        public long? Search { get; set; }
    }
}

