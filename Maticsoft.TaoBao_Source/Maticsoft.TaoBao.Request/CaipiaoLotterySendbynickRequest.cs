namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CaipiaoLotterySendbynickRequest : ITopRequest<CaipiaoLotterySendbynickResponse>
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
            return "taobao.caipiao.lottery.sendbynick";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("buyer_nick", this.BuyerNick);
            dictionary.Add("lottery_type_id", this.LotteryTypeId);
            dictionary.Add("stake_count", this.StakeCount);
            dictionary.Add("sweety_words", this.SweetyWords);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("buyer_nick", this.BuyerNick);
            RequestValidator.ValidateRequired("stake_count", this.StakeCount);
        }

        public string BuyerNick { get; set; }

        public long? LotteryTypeId { get; set; }

        public long? StakeCount { get; set; }

        public string SweetyWords { get; set; }
    }
}

