namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoProductGradepriceUpdateRequest : ITopRequest<FenxiaoProductGradepriceUpdateResponse>
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
            return "taobao.fenxiao.product.gradeprice.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("ids", this.Ids);
            dictionary.Add("prices", this.Prices);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("sku_id", this.SkuId);
            dictionary.Add("target_type", this.TargetType);
            dictionary.Add("trade_type", this.TradeType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("ids", this.Ids);
            RequestValidator.ValidateMaxListSize("ids", this.Ids, 200);
            RequestValidator.ValidateRequired("prices", this.Prices);
            RequestValidator.ValidateMaxListSize("prices", this.Prices, 200);
            RequestValidator.ValidateRequired("product_id", this.ProductId);
            RequestValidator.ValidateRequired("target_type", this.TargetType);
        }

        public string Ids { get; set; }

        public string Prices { get; set; }

        public long? ProductId { get; set; }

        public long? SkuId { get; set; }

        public string TargetType { get; set; }

        public string TradeType { get; set; }
    }
}

