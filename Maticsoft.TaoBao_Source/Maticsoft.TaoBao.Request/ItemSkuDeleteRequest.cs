namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemSkuDeleteRequest : ITopRequest<ItemSkuDeleteResponse>
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
            return "taobao.item.sku.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("item_num", this.ItemNum);
            dictionary.Add("item_price", this.ItemPrice);
            dictionary.Add("lang", this.Lang);
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("properties", this.Properties);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateRequired("properties", this.Properties);
        }

        public long? ItemNum { get; set; }

        public string ItemPrice { get; set; }

        public string Lang { get; set; }

        public long? NumIid { get; set; }

        public string Properties { get; set; }
    }
}

