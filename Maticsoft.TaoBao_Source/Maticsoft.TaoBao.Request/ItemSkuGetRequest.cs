namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemSkuGetRequest : ITopRequest<ItemSkuGetResponse>
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
            return "taobao.item.sku.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("fields", this.Fields);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("sku_id", this.SkuId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 0L);
            RequestValidator.ValidateRequired("sku_id", this.SkuId);
        }

        public string Fields { get; set; }

        public string Nick { get; set; }

        public long? NumIid { get; set; }

        public long? SkuId { get; set; }
    }
}

