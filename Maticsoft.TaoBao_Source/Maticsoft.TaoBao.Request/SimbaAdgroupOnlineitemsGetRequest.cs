namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaAdgroupOnlineitemsGetRequest : ITopRequest<SimbaAdgroupOnlineitemsGetResponse>
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
            return "taobao.simba.adgroup.onlineitems.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("nick", this.Nick);
            dictionary.Add("order_by", this.OrderBy);
            dictionary.Add("order_field", this.OrderField);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxValue("page_no", this.PageNo, 50L);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 200L);
        }

        public string Nick { get; set; }

        public bool? OrderBy { get; set; }

        public string OrderField { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }
    }
}

