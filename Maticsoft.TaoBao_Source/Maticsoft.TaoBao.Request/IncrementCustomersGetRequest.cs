namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class IncrementCustomersGetRequest : ITopRequest<IncrementCustomersGetResponse>
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
            return "taobao.increment.customers.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("fields", this.Fields);
            dictionary.Add("nicks", this.Nicks);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxListSize("nicks", this.Nicks, 20);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 0L);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 200L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 0L);
            RequestValidator.ValidateMaxListSize("type", this.Type, 3);
        }

        public string Fields { get; set; }

        public string Nicks { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string Type { get; set; }
    }
}

