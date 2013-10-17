namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemQuantityUpdateRequest : ITopRequest<ItemQuantityUpdateResponse>
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
            return "taobao.item.quantity.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("outer_id", this.OuterId);
            dictionary.Add("quantity", this.Quantity);
            dictionary.Add("sku_id", this.SkuId);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateRequired("quantity", this.Quantity);
        }

        public long? NumIid { get; set; }

        public string OuterId { get; set; }

        public long? Quantity { get; set; }

        public long? SkuId { get; set; }

        public long? Type { get; set; }
    }
}

