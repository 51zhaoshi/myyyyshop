namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TradeOrderskuUpdateRequest : ITopRequest<TradeOrderskuUpdateResponse>
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
            return "taobao.trade.ordersku.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("oid", this.Oid);
            dictionary.Add("sku_id", this.SkuId);
            dictionary.Add("sku_props", this.SkuProps);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("oid", this.Oid);
        }

        public long? Oid { get; set; }

        public long? SkuId { get; set; }

        public string SkuProps { get; set; }
    }
}

