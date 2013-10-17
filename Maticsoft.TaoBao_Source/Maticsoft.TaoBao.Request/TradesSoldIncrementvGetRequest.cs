namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TradesSoldIncrementvGetRequest : ITopRequest<TradesSoldIncrementvGetResponse>
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
            return "taobao.trades.sold.incrementv.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_create", this.EndCreate);
            dictionary.Add("ext_type", this.ExtType);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("start_create", this.StartCreate);
            dictionary.Add("status", this.Status);
            dictionary.Add("tag", this.Tag);
            dictionary.Add("type", this.Type);
            dictionary.Add("use_has_next", this.UseHasNext);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("end_create", this.EndCreate);
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("start_create", this.StartCreate);
        }

        public DateTime? EndCreate { get; set; }

        public string ExtType { get; set; }

        public string Fields { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public DateTime? StartCreate { get; set; }

        public string Status { get; set; }

        public string Tag { get; set; }

        public string Type { get; set; }

        public bool? UseHasNext { get; set; }
    }
}

