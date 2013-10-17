namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PromotionLimitdiscountGetRequest : ITopRequest<PromotionLimitdiscountGetResponse>
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
            return "taobao.promotion.limitdiscount.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("limit_discount_id", this.LimitDiscountId);
            dictionary.Add("page_number", this.PageNumber);
            dictionary.Add("start_time", this.StartTime);
            dictionary.Add("status", this.Status);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public DateTime? EndTime { get; set; }

        public long? LimitDiscountId { get; set; }

        public long? PageNumber { get; set; }

        public DateTime? StartTime { get; set; }

        public string Status { get; set; }
    }
}

