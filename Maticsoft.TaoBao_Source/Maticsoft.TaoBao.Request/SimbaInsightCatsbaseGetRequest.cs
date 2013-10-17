namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaInsightCatsbaseGetRequest : ITopRequest<SimbaInsightCatsbaseGetResponse>
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
            return "taobao.simba.insight.catsbase.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("category_ids", this.CategoryIds);
            dictionary.Add("filter", this.Filter);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("time", this.Time);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("category_ids", this.CategoryIds);
            RequestValidator.ValidateMaxListSize("category_ids", this.CategoryIds, 200);
            RequestValidator.ValidateRequired("filter", this.Filter);
            RequestValidator.ValidateRequired("time", this.Time);
        }

        public string CategoryIds { get; set; }

        public string Filter { get; set; }

        public string Nick { get; set; }

        public string Time { get; set; }
    }
}

