namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TradeReceivetimeDelayRequest : ITopRequest<TradeReceivetimeDelayResponse>
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
            return "taobao.trade.receivetime.delay";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("days", this.Days);
            dictionary.Add("tid", this.Tid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("days", this.Days);
            RequestValidator.ValidateMaxValue("days", this.Days, 10L);
            RequestValidator.ValidateMinValue("days", this.Days, 3L);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        public long? Days { get; set; }

        public long? Tid { get; set; }
    }
}

