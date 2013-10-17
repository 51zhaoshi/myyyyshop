namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaInsightWordscatsGetRequest : ITopRequest<SimbaInsightWordscatsGetResponse>
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
            return "taobao.simba.insight.wordscats.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("filter", this.Filter);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("word_categories", this.WordCategories);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("filter", this.Filter);
            RequestValidator.ValidateRequired("word_categories", this.WordCategories);
            RequestValidator.ValidateMaxListSize("word_categories", this.WordCategories, 200);
        }

        public string Filter { get; set; }

        public string Nick { get; set; }

        public string WordCategories { get; set; }
    }
}

