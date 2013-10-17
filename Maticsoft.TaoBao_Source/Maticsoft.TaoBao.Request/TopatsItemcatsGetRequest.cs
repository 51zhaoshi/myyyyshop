namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TopatsItemcatsGetRequest : ITopRequest<TopatsItemcatsGetResponse>
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
            return "taobao.topats.itemcats.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cids", this.Cids);
            dictionary.Add("output_format", this.OutputFormat);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxValue("type", this.Type, 2L);
            RequestValidator.ValidateMinValue("type", this.Type, 1L);
        }

        public string Cids { get; set; }

        public string OutputFormat { get; set; }

        public long? Type { get; set; }
    }
}

