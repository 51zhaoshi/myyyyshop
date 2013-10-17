namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemConsignmentDeleteRequest : ITopRequest<WlbItemConsignmentDeleteResponse>
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
            return "taobao.wlb.item.consignment.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("ic_item_id", this.IcItemId);
            dictionary.Add("owner_item_id", this.OwnerItemId);
            dictionary.Add("rule_id", this.RuleId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("ic_item_id", this.IcItemId);
            RequestValidator.ValidateRequired("owner_item_id", this.OwnerItemId);
            RequestValidator.ValidateRequired("rule_id", this.RuleId);
        }

        public long? IcItemId { get; set; }

        public long? OwnerItemId { get; set; }

        public long? RuleId { get; set; }
    }
}

