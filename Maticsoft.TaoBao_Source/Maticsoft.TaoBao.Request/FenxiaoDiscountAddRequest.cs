namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoDiscountAddRequest : ITopRequest<FenxiaoDiscountAddResponse>
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
            return "taobao.fenxiao.discount.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("discount_name", this.DiscountName);
            dictionary.Add("discount_types", this.DiscountTypes);
            dictionary.Add("discount_values", this.DiscountValues);
            dictionary.Add("target_ids", this.TargetIds);
            dictionary.Add("target_types", this.TargetTypes);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("discount_name", this.DiscountName);
            RequestValidator.ValidateRequired("discount_types", this.DiscountTypes);
            RequestValidator.ValidateRequired("discount_values", this.DiscountValues);
            RequestValidator.ValidateRequired("target_ids", this.TargetIds);
            RequestValidator.ValidateRequired("target_types", this.TargetTypes);
        }

        public string DiscountName { get; set; }

        public string DiscountTypes { get; set; }

        public string DiscountValues { get; set; }

        public string TargetIds { get; set; }

        public string TargetTypes { get; set; }
    }
}

