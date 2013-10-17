namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CaipiaoLotterySendRequest : ITopRequest<CaipiaoLotterySendResponse>
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
            return "taobao.caipiao.lottery.send";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("buyer_num_id", this.BuyerNumId);
            dictionary.Add("lottery_type_id", this.LotteryTypeId);
            dictionary.Add("seller_num_id", this.SellerNumId);
            dictionary.Add("stake_count", this.StakeCount);
            dictionary.Add("sweety_words", this.SweetyWords);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("buyer_num_id", this.BuyerNumId);
            RequestValidator.ValidateRequired("seller_num_id", this.SellerNumId);
            RequestValidator.ValidateRequired("stake_count", this.StakeCount);
        }

        public long? BuyerNumId { get; set; }

        public long? LotteryTypeId { get; set; }

        public long? SellerNumId { get; set; }

        public long? StakeCount { get; set; }

        public string SweetyWords { get; set; }
    }
}

