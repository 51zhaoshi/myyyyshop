namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TmallItemsDiscountSearchRequest : ITopRequest<TmallItemsDiscountSearchResponse>
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
            return "tmall.items.discount.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("auction_tag", this.AuctionTag);
            dictionary.Add("brand", this.Brand);
            dictionary.Add("cat", this.Cat);
            dictionary.Add("end_price", this.EndPrice);
            dictionary.Add("post_fee", this.PostFee);
            dictionary.Add("q", this.Q);
            dictionary.Add("sort", this.Sort);
            dictionary.Add("start", this.Start);
            dictionary.Add("start_price", this.StartPrice);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxValue("start", this.Start, 0x3e8L);
        }

        public long? AuctionTag { get; set; }

        public long? Brand { get; set; }

        public string Cat { get; set; }

        public string EndPrice { get; set; }

        public long? PostFee { get; set; }

        public string Q { get; set; }

        public string Sort { get; set; }

        public long? Start { get; set; }

        public string StartPrice { get; set; }
    }
}

