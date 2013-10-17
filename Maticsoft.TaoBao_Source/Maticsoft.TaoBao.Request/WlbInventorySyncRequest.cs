namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbInventorySyncRequest : ITopRequest<WlbInventorySyncResponse>
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
            return "taobao.wlb.inventory.sync";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("item_type", this.ItemType);
            dictionary.Add("quantity", this.Quantity);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("item_id", this.ItemId);
            RequestValidator.ValidateRequired("item_type", this.ItemType);
            RequestValidator.ValidateRequired("quantity", this.Quantity);
        }

        public long? ItemId { get; set; }

        public string ItemType { get; set; }

        public long? Quantity { get; set; }
    }
}

