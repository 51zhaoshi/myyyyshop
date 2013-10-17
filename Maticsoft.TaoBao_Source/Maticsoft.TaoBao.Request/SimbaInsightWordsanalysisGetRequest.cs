namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaInsightWordsanalysisGetRequest : ITopRequest<SimbaInsightWordsanalysisGetResponse>
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
            return "taobao.simba.insight.wordsanalysis.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("nick", this.Nick);
            dictionary.Add("stu", this.Stu);
            dictionary.Add("words", this.Words);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("stu", this.Stu);
            RequestValidator.ValidateRequired("words", this.Words);
            RequestValidator.ValidateMaxListSize("words", this.Words, 200);
        }

        public string Nick { get; set; }

        public string Stu { get; set; }

        public string Words { get; set; }
    }
}

