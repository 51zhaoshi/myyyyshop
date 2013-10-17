namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PromotionCoupondetailGetRequest : ITopRequest<PromotionCoupondetailGetResponse>
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
            return "taobao.promotion.coupondetail.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("buyer_nick", this.BuyerNick);
            dictionary.Add("coupon_id", this.CouponId);
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("state", this.State);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("coupon_id", this.CouponId);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 20L);
        }

        public string BuyerNick { get; set; }

        public long? CouponId { get; set; }

        public DateTime? EndTime { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string State { get; set; }
    }
}

