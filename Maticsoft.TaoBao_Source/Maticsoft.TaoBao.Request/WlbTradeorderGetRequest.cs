namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbTradeorderGetRequest : ITopRequest<WlbTradeorderGetResponse>
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
            return "taobao.wlb.tradeorder.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("trade_id", this.TradeId);
            dictionary.Add("trade_type", this.TradeType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("trade_id", this.TradeId);
            RequestValidator.ValidateRequired("trade_type", this.TradeType);
        }

        public string TradeId { get; set; }

        public string TradeType { get; set; }
    }
}

