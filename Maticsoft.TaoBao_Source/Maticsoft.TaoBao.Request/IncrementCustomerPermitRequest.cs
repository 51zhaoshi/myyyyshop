namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class IncrementCustomerPermitRequest : ITopRequest<IncrementCustomerPermitResponse>
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
            return "taobao.increment.customer.permit";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("status", this.Status);
            dictionary.Add("topics", this.Topics);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxListSize("type", this.Type, 3);
        }

        public string Status { get; set; }

        public string Topics { get; set; }

        public string Type { get; set; }
    }
}

