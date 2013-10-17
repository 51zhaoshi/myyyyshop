namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TaobaokeItemsCouponGetRequest : ITopRequest<TaobaokeItemsCouponGetResponse>
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
            return "taobao.taobaoke.items.coupon.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("area", this.Area);
            dictionary.Add("cid", this.Cid);
            dictionary.Add("coupon_type", this.CouponType);
            dictionary.Add("end_commission_num", this.EndCommissionNum);
            dictionary.Add("end_commission_rate", this.EndCommissionRate);
            dictionary.Add("end_commission_volume", this.EndCommissionVolume);
            dictionary.Add("end_coupon_rate", this.EndCouponRate);
            dictionary.Add("end_credit", this.EndCredit);
            dictionary.Add("end_volume", this.EndVolume);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("is_mobile", this.IsMobile);
            dictionary.Add("keyword", this.Keyword);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("outer_code", this.OuterCode);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("pid", this.Pid);
            dictionary.Add("shop_type", this.ShopType);
            dictionary.Add("sort", this.Sort);
            dictionary.Add("start_commission_num", this.StartCommissionNum);
            dictionary.Add("start_commission_rate", this.StartCommissionRate);
            dictionary.Add("start_commission_volume", this.StartCommissionVolume);
            dictionary.Add("start_coupon_rate", this.StartCouponRate);
            dictionary.Add("start_credit", this.StartCredit);
            dictionary.Add("start_volume", this.StartVolume);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
        }

        public string Area { get; set; }

        public long? Cid { get; set; }

        public long? CouponType { get; set; }

        public long? EndCommissionNum { get; set; }

        public long? EndCommissionRate { get; set; }

        public long? EndCommissionVolume { get; set; }

        public long? EndCouponRate { get; set; }

        public string EndCredit { get; set; }

        public long? EndVolume { get; set; }

        public string Fields { get; set; }

        public bool? IsMobile { get; set; }

        public string Keyword { get; set; }

        public string Nick { get; set; }

        public string OuterCode { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public long? Pid { get; set; }

        public string ShopType { get; set; }

        public string Sort { get; set; }

        public long? StartCommissionNum { get; set; }

        public long? StartCommissionRate { get; set; }

        public long? StartCommissionVolume { get; set; }

        public long? StartCouponRate { get; set; }

        public string StartCredit { get; set; }

        public long? StartVolume { get; set; }
    }
}

