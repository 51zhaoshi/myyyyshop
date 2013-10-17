namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemSynchronizeDeleteRequest : ITopRequest<WlbItemSynchronizeDeleteResponse>
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
            return "taobao.wlb.item.synchronize.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("ext_entity_id", this.ExtEntityId);
            dictionary.Add("ext_entity_type", this.ExtEntityType);
            dictionary.Add("item_id", this.ItemId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("ext_entity_id", this.ExtEntityId);
            RequestValidator.ValidateRequired("ext_entity_type", this.ExtEntityType);
            RequestValidator.ValidateRequired("item_id", this.ItemId);
        }

        public long? ExtEntityId { get; set; }

        public string ExtEntityType { get; set; }

        public long? ItemId { get; set; }
    }
}

