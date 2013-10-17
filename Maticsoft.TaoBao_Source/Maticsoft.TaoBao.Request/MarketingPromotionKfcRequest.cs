namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MarketingPromotionKfcRequest : ITopRequest<MarketingPromotionKfcResponse>
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
            return "taobao.marketing.promotion.kfc";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("promotion_desc", this.PromotionDesc);
            dictionary.Add("promotion_title", this.PromotionTitle);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("promotion_desc", this.PromotionDesc);
            RequestValidator.ValidateRequired("promotion_title", this.PromotionTitle);
        }

        public string PromotionDesc { get; set; }

        public string PromotionTitle { get; set; }
    }
}

