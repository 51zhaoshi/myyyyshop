namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoDiscountUpdateRequest : ITopRequest<FenxiaoDiscountUpdateResponse>
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
            return "taobao.fenxiao.discount.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("detail_ids", this.DetailIds);
            dictionary.Add("detail_statuss", this.DetailStatuss);
            dictionary.Add("discount_id", this.DiscountId);
            dictionary.Add("discount_name", this.DiscountName);
            dictionary.Add("discount_status", this.DiscountStatus);
            dictionary.Add("discount_types", this.DiscountTypes);
            dictionary.Add("discount_values", this.DiscountValues);
            dictionary.Add("target_ids", this.TargetIds);
            dictionary.Add("target_types", this.TargetTypes);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public string DetailIds { get; set; }

        public string DetailStatuss { get; set; }

        public long? DiscountId { get; set; }

        public string DiscountName { get; set; }

        public string DiscountStatus { get; set; }

        public string DiscountTypes { get; set; }

        public string DiscountValues { get; set; }

        public string TargetIds { get; set; }

        public string TargetTypes { get; set; }
    }
}

