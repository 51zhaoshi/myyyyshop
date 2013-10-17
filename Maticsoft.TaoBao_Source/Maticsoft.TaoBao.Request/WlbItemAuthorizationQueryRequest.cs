namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemAuthorizationQueryRequest : ITopRequest<WlbItemAuthorizationQueryResponse>
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
            return "taobao.wlb.item.authorization.query";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("name", this.Name);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("rule_code", this.RuleCode);
            dictionary.Add("status", this.Status);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("name", this.Name, 0xff);
        }

        public long? ItemId { get; set; }

        public string Name { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string RuleCode { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
    }
}

