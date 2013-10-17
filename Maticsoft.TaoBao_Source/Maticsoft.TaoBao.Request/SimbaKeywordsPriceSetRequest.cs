namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaKeywordsPriceSetRequest : ITopRequest<SimbaKeywordsPriceSetResponse>
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
            return "taobao.simba.keywords.price.set";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_id", this.AdgroupId);
            dictionary.Add("keywordid_prices", this.KeywordidPrices);
            dictionary.Add("nick", this.Nick);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("keywordid_prices", this.KeywordidPrices);
            RequestValidator.ValidateMaxListSize("keywordid_prices", this.KeywordidPrices, 100);
        }

        public long? AdgroupId { get; set; }

        public string KeywordidPrices { get; set; }

        public string Nick { get; set; }
    }
}

