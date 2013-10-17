namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoProductSkuAddRequest : ITopRequest<FenxiaoProductSkuAddResponse>
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
            return "taobao.fenxiao.product.sku.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("agent_cost_price", this.AgentCostPrice);
            dictionary.Add("dealer_cost_price", this.DealerCostPrice);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("properties", this.Properties);
            dictionary.Add("quantity", this.Quantity);
            dictionary.Add("sku_number", this.SkuNumber);
            dictionary.Add("standard_price", this.StandardPrice);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("product_id", this.ProductId);
            RequestValidator.ValidateRequired("properties", this.Properties);
            RequestValidator.ValidateMaxValue("quantity", this.Quantity, 0xf4240L);
            RequestValidator.ValidateMinValue("quantity", this.Quantity, 0L);
            RequestValidator.ValidateRequired("standard_price", this.StandardPrice);
        }

        public string AgentCostPrice { get; set; }

        public string DealerCostPrice { get; set; }

        public long? ProductId { get; set; }

        public string Properties { get; set; }

        public long? Quantity { get; set; }

        public string SkuNumber { get; set; }

        public string StandardPrice { get; set; }
    }
}

