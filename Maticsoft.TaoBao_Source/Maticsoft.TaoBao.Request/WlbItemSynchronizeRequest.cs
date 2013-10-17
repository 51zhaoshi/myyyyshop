namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemSynchronizeRequest : ITopRequest<WlbItemSynchronizeResponse>
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
            return "taobao.wlb.item.synchronize";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("ext_entity_id", this.ExtEntityId);
            dictionary.Add("ext_entity_type", this.ExtEntityType);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("user_nick", this.UserNick);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("ext_entity_id", this.ExtEntityId);
            RequestValidator.ValidateRequired("ext_entity_type", this.ExtEntityType);
            RequestValidator.ValidateRequired("item_id", this.ItemId);
            RequestValidator.ValidateRequired("user_nick", this.UserNick);
            RequestValidator.ValidateMaxLength("user_nick", this.UserNick, 0x40);
        }

        public long? ExtEntityId { get; set; }

        public string ExtEntityType { get; set; }

        public long? ItemId { get; set; }

        public string UserNick { get; set; }
    }
}

