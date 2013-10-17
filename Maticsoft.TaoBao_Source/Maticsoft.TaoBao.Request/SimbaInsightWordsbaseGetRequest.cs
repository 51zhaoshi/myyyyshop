namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaInsightWordsbaseGetRequest : ITopRequest<SimbaInsightWordsbaseGetResponse>
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
            return "taobao.simba.insight.wordsbase.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("filter", this.Filter);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("time", this.Time);
            dictionary.Add("words", this.Words);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("filter", this.Filter);
            RequestValidator.ValidateRequired("time", this.Time);
            RequestValidator.ValidateRequired("words", this.Words);
            RequestValidator.ValidateMaxListSize("words", this.Words, 170);
        }

        public string Filter { get; set; }

        public string Nick { get; set; }

        public string Time { get; set; }

        public string Words { get; set; }
    }
}

