namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemcatsIncrementGetRequest : ITopRequest<ItemcatsIncrementGetResponse>
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
            return "taobao.itemcats.increment.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cids", this.Cids);
            dictionary.Add("days", this.Days);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cids", this.Cids);
            RequestValidator.ValidateMaxListSize("cids", this.Cids, 0x3e8);
            RequestValidator.ValidateMaxValue("days", this.Days, 7L);
            RequestValidator.ValidateMinValue("days", this.Days, 1L);
            RequestValidator.ValidateMaxValue("type", this.Type, 2L);
            RequestValidator.ValidateMinValue("type", this.Type, 1L);
        }

        public string Cids { get; set; }

        public long? Days { get; set; }

        public long? Type { get; set; }
    }
}

