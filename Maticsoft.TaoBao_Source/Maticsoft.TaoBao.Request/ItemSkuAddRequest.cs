namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemSkuAddRequest : ITopRequest<ItemSkuAddResponse>
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
            return "taobao.item.sku.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("item_price", this.ItemPrice);
            dictionary.Add("lang", this.Lang);
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("outer_id", this.OuterId);
            dictionary.Add("price", this.Price);
            dictionary.Add("properties", this.Properties);
            dictionary.Add("quantity", this.Quantity);
            dictionary.Add("spec_id", this.SpecId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 0L);
            RequestValidator.ValidateRequired("price", this.Price);
            RequestValidator.ValidateRequired("properties", this.Properties);
            RequestValidator.ValidateRequired("quantity", this.Quantity);
            RequestValidator.ValidateMinValue("quantity", this.Quantity, 0L);
        }

        public string ItemPrice { get; set; }

        public string Lang { get; set; }

        public long? NumIid { get; set; }

        public string OuterId { get; set; }

        public string Price { get; set; }

        public string Properties { get; set; }

        public long? Quantity { get; set; }

        public string SpecId { get; set; }
    }
}

