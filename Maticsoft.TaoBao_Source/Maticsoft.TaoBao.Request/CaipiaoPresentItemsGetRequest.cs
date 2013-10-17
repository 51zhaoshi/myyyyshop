namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CaipiaoPresentItemsGetRequest : ITopRequest<CaipiaoPresentItemsGetResponse>
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
            return "taobao.caipiao.present.items.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_date", this.EndDate);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("start_date", this.StartDate);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("end_date", this.EndDate);
            RequestValidator.ValidateRequired("page_no", this.PageNo);
            RequestValidator.ValidateRequired("page_size", this.PageSize);
            RequestValidator.ValidateRequired("start_date", this.StartDate);
        }

        public string EndDate { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string StartDate { get; set; }
    }
}

