namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaInsightCatsanalysisGetRequest : ITopRequest<SimbaInsightCatsanalysisGetResponse>
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
            return "taobao.simba.insight.catsanalysis.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("category_ids", this.CategoryIds);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("stu", this.Stu);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("category_ids", this.CategoryIds);
            RequestValidator.ValidateMaxListSize("category_ids", this.CategoryIds, 200);
            RequestValidator.ValidateRequired("stu", this.Stu);
        }

        public string CategoryIds { get; set; }

        public string Nick { get; set; }

        public string Stu { get; set; }
    }
}

