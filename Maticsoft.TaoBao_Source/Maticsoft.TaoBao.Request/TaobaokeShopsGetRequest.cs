namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TaobaokeShopsGetRequest : ITopRequest<TaobaokeShopsGetResponse>
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
            return "taobao.taobaoke.shops.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cid", this.Cid);
            dictionary.Add("end_auctioncount", this.EndAuctioncount);
            dictionary.Add("end_commissionrate", this.EndCommissionrate);
            dictionary.Add("end_credit", this.EndCredit);
            dictionary.Add("end_totalaction", this.EndTotalaction);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("is_mobile", this.IsMobile);
            dictionary.Add("keyword", this.Keyword);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("only_mall", this.OnlyMall);
            dictionary.Add("outer_code", this.OuterCode);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("pid", this.Pid);
            dictionary.Add("sort_field", this.SortField);
            dictionary.Add("sort_type", this.SortType);
            dictionary.Add("start_auctioncount", this.StartAuctioncount);
            dictionary.Add("start_commissionrate", this.StartCommissionrate);
            dictionary.Add("start_credit", this.StartCredit);
            dictionary.Add("start_totalaction", this.StartTotalaction);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
        }

        public long? Cid { get; set; }

        public string EndAuctioncount { get; set; }

        public string EndCommissionrate { get; set; }

        public string EndCredit { get; set; }

        public string EndTotalaction { get; set; }

        public string Fields { get; set; }

        public bool? IsMobile { get; set; }

        public string Keyword { get; set; }

        public string Nick { get; set; }

        public bool? OnlyMall { get; set; }

        public string OuterCode { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public long? Pid { get; set; }

        public string SortField { get; set; }

        public string SortType { get; set; }

        public string StartAuctioncount { get; set; }

        public string StartCommissionrate { get; set; }

        public string StartCredit { get; set; }

        public string StartTotalaction { get; set; }
    }
}

