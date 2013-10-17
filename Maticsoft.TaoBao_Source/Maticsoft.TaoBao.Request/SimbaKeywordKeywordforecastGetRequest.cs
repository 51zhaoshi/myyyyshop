namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaKeywordKeywordforecastGetRequest : ITopRequest<SimbaKeywordKeywordforecastGetResponse>
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
            return "taobao.simba.keyword.keywordforecast.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("bidword_price", this.BidwordPrice);
            dictionary.Add("keyword_id", this.KeywordId);
            dictionary.Add("nick", this.Nick);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("bidword_price", this.BidwordPrice);
            RequestValidator.ValidateRequired("keyword_id", this.KeywordId);
        }

        public long? BidwordPrice { get; set; }

        public long? KeywordId { get; set; }

        public string Nick { get; set; }
    }
}

