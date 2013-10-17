namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RefundsApplyGetRequest : ITopRequest<RefundsApplyGetResponse>
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
            return "taobao.refunds.apply.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("fields", this.Fields);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("seller_nick", this.SellerNick);
            dictionary.Add("status", this.Status);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1L);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 100L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
        }

        public string Fields { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string SellerNick { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
    }
}

