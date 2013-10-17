namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaInsightCatsrelatedwordGetRequest : ITopRequest<SimbaInsightCatsrelatedwordGetResponse>
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
            return "taobao.simba.insight.catsrelatedword.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("nick", this.Nick);
            dictionary.Add("result_num", this.ResultNum);
            dictionary.Add("words", this.Words);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("result_num", this.ResultNum);
            RequestValidator.ValidateMaxValue("result_num", this.ResultNum, 10L);
            RequestValidator.ValidateMinValue("result_num", this.ResultNum, 1L);
            RequestValidator.ValidateRequired("words", this.Words);
            RequestValidator.ValidateMaxListSize("words", this.Words, 200);
        }

        public string Nick { get; set; }

        public long? ResultNum { get; set; }

        public string Words { get; set; }
    }
}

