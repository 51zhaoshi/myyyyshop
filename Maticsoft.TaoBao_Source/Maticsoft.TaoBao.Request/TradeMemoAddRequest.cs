namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TradeMemoAddRequest : ITopRequest<TradeMemoAddResponse>
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
            return "taobao.trade.memo.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("flag", this.Flag);
            dictionary.Add("memo", this.Memo);
            dictionary.Add("tid", this.Tid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxValue("flag", this.Flag, 5L);
            RequestValidator.ValidateMinValue("flag", this.Flag, 0L);
            RequestValidator.ValidateRequired("memo", this.Memo);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        public long? Flag { get; set; }

        public string Memo { get; set; }

        public long? Tid { get; set; }
    }
}

