namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbTmsorderQueryRequest : ITopRequest<WlbTmsorderQueryResponse>
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
            return "taobao.wlb.tmsorder.query";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("order_code", this.OrderCode);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("order_code", this.OrderCode);
            RequestValidator.ValidateMaxLength("order_code", this.OrderCode, 0x40);
        }

        public string OrderCode { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }
    }
}

