namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TraderateListAddRequest : ITopRequest<TraderateListAddResponse>
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
            return "taobao.traderate.list.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("anony", this.Anony);
            dictionary.Add("content", this.Content);
            dictionary.Add("result", this.Result);
            dictionary.Add("role", this.Role);
            dictionary.Add("tid", this.Tid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("result", this.Result);
            RequestValidator.ValidateRequired("role", this.Role);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        public bool? Anony { get; set; }

        public string Content { get; set; }

        public string Result { get; set; }

        public string Role { get; set; }

        public long? Tid { get; set; }
    }
}

