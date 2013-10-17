namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemConsignmentCreateRequest : ITopRequest<WlbItemConsignmentCreateResponse>
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
            return "taobao.wlb.item.consignment.create";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("number", this.Number);
            dictionary.Add("owner_item_id", this.OwnerItemId);
            dictionary.Add("owner_user_id", this.OwnerUserId);
            dictionary.Add("rule_id", this.RuleId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("item_id", this.ItemId);
            RequestValidator.ValidateRequired("number", this.Number);
            RequestValidator.ValidateRequired("owner_item_id", this.OwnerItemId);
            RequestValidator.ValidateRequired("owner_user_id", this.OwnerUserId);
            RequestValidator.ValidateRequired("rule_id", this.RuleId);
        }

        public long? ItemId { get; set; }

        public long? Number { get; set; }

        public long? OwnerItemId { get; set; }

        public long? OwnerUserId { get; set; }

        public long? RuleId { get; set; }
    }
}

