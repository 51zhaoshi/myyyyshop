namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TaobaokeShopsRelateGetRequest : ITopRequest<TaobaokeShopsRelateGetResponse>
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
            return "taobao.taobaoke.shops.relate.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("fields", this.Fields);
            dictionary.Add("is_mobile", this.IsMobile);
            dictionary.Add("max_count", this.MaxCount);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("outer_code", this.OuterCode);
            dictionary.Add("pid", this.Pid);
            dictionary.Add("seller_id", this.SellerId);
            dictionary.Add("seller_nick", this.SellerNick);
            dictionary.Add("shop_type", this.ShopType);
            dictionary.Add("sort", this.Sort);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
        }

        public string Fields { get; set; }

        public bool? IsMobile { get; set; }

        public long? MaxCount { get; set; }

        public string Nick { get; set; }

        public string OuterCode { get; set; }

        public long? Pid { get; set; }

        public long? SellerId { get; set; }

        public string SellerNick { get; set; }

        public string ShopType { get; set; }

        public string Sort { get; set; }
    }
}

