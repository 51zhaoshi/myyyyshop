namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TaobaokeItemsGetRequest : ITopRequest<TaobaokeItemsGetResponse>
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
            return "taobao.taobaoke.items.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("area", this.Area);
            dictionary.Add("auto_send", this.AutoSend);
            dictionary.Add("cash_coupon", this.CashCoupon);
            dictionary.Add("cash_ondelivery", this.CashOndelivery);
            dictionary.Add("cid", this.Cid);
            dictionary.Add("end_commissionNum", this.EndCommissionNum);
            dictionary.Add("end_commissionRate", this.EndCommissionRate);
            dictionary.Add("end_credit", this.EndCredit);
            dictionary.Add("end_price", this.EndPrice);
            dictionary.Add("end_totalnum", this.EndTotalnum);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("guarantee", this.Guarantee);
            dictionary.Add("is_mobile", this.IsMobile);
            dictionary.Add("keyword", this.Keyword);
            dictionary.Add("mall_item", this.MallItem);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("onemonth_repair", this.OnemonthRepair);
            dictionary.Add("outer_code", this.OuterCode);
            dictionary.Add("overseas_item", this.OverseasItem);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("pid", this.Pid);
            dictionary.Add("real_describe", this.RealDescribe);
            dictionary.Add("sevendays_return", this.SevendaysReturn);
            dictionary.Add("sort", this.Sort);
            dictionary.Add("start_commissionNum", this.StartCommissionNum);
            dictionary.Add("start_commissionRate", this.StartCommissionRate);
            dictionary.Add("start_credit", this.StartCredit);
            dictionary.Add("start_price", this.StartPrice);
            dictionary.Add("start_totalnum", this.StartTotalnum);
            dictionary.Add("vip_card", this.VipCard);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxValue("cid", this.Cid, 0x7fffffffL);
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxValue("page_no", this.PageNo, 10L);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 400L);
        }

        public string Area { get; set; }

        public string AutoSend { get; set; }

        public string CashCoupon { get; set; }

        public string CashOndelivery { get; set; }

        public long? Cid { get; set; }

        public string EndCommissionNum { get; set; }

        public string EndCommissionRate { get; set; }

        public string EndCredit { get; set; }

        public string EndPrice { get; set; }

        public string EndTotalnum { get; set; }

        public string Fields { get; set; }

        public string Guarantee { get; set; }

        public bool? IsMobile { get; set; }

        public string Keyword { get; set; }

        public string MallItem { get; set; }

        public string Nick { get; set; }

        public string OnemonthRepair { get; set; }

        public string OuterCode { get; set; }

        public string OverseasItem { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public long? Pid { get; set; }

        public string RealDescribe { get; set; }

        public string SevendaysReturn { get; set; }

        public string Sort { get; set; }

        public string StartCommissionNum { get; set; }

        public string StartCommissionRate { get; set; }

        public string StartCredit { get; set; }

        public string StartPrice { get; set; }

        public string StartTotalnum { get; set; }

        public string VipCard { get; set; }
    }
}

