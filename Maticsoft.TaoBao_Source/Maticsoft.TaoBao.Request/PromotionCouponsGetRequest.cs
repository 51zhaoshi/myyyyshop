namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PromotionCouponsGetRequest : ITopRequest<PromotionCouponsGetResponse>
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
            return "taobao.promotion.coupons.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("coupon_id", this.CouponId);
            dictionary.Add("denominations", this.Denominations);
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxValue("denominations", this.Denominations, 100L);
            RequestValidator.ValidateMinValue("denominations", this.Denominations, 3L);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1L);
        }

        public long? CouponId { get; set; }

        public long? Denominations { get; set; }

        public DateTime? EndTime { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }
    }
}

