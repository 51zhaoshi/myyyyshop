namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TraderatesGetRequest : ITopRequest<TraderatesGetResponse>
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
            return "taobao.traderates.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_date", this.EndDate);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("rate_type", this.RateType);
            dictionary.Add("result", this.Result);
            dictionary.Add("role", this.Role);
            dictionary.Add("start_date", this.StartDate);
            dictionary.Add("tid", this.Tid);
            dictionary.Add("use_has_next", this.UseHasNext);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 150L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
            RequestValidator.ValidateRequired("rate_type", this.RateType);
            RequestValidator.ValidateRequired("role", this.Role);
        }

        public DateTime? EndDate { get; set; }

        public string Fields { get; set; }

        public long? NumIid { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string RateType { get; set; }

        public string Result { get; set; }

        public string Role { get; set; }

        public DateTime? StartDate { get; set; }

        public long? Tid { get; set; }

        public bool? UseHasNext { get; set; }
    }
}

