namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemAuthorizationAddRequest : ITopRequest<WlbItemAuthorizationAddResponse>
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
            return "taobao.wlb.item.authorization.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("auth_type", this.AuthType);
            dictionary.Add("authorize_end_time", this.AuthorizeEndTime);
            dictionary.Add("authorize_start_time", this.AuthorizeStartTime);
            dictionary.Add("consign_user_nick", this.ConsignUserNick);
            dictionary.Add("item_id_list", this.ItemIdList);
            dictionary.Add("name", this.Name);
            dictionary.Add("quantity", this.Quantity);
            dictionary.Add("rule_code", this.RuleCode);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("auth_type", this.AuthType);
            RequestValidator.ValidateRequired("authorize_end_time", this.AuthorizeEndTime);
            RequestValidator.ValidateRequired("authorize_start_time", this.AuthorizeStartTime);
            RequestValidator.ValidateRequired("consign_user_nick", this.ConsignUserNick);
            RequestValidator.ValidateRequired("item_id_list", this.ItemIdList);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 0xff);
            RequestValidator.ValidateRequired("rule_code", this.RuleCode);
        }

        public DateTime? AuthorizeEndTime { get; set; }

        public DateTime? AuthorizeStartTime { get; set; }

        public long? AuthType { get; set; }

        public string ConsignUserNick { get; set; }

        public string ItemIdList { get; set; }

        public string Name { get; set; }

        public long? Quantity { get; set; }

        public string RuleCode { get; set; }
    }
}

