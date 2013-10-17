namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItempropvaluesGetRequest : ITopRequest<ItempropvaluesGetResponse>
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
            return "taobao.itempropvalues.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cid", this.Cid);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("pvs", this.Pvs);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cid", this.Cid);
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxValue("type", this.Type, 2L);
            RequestValidator.ValidateMinValue("type", this.Type, 1L);
        }

        public long? Cid { get; set; }

        public string Fields { get; set; }

        public string Pvs { get; set; }

        public long? Type { get; set; }
    }
}

