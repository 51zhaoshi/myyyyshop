namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoDiscountsGetRequest : ITopRequest<FenxiaoDiscountsGetResponse>
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
            return "taobao.fenxiao.discounts.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("discount_id", this.DiscountId);
            dictionary.Add("ext_fields", this.ExtFields);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public long? DiscountId { get; set; }

        public string ExtFields { get; set; }
    }
}

