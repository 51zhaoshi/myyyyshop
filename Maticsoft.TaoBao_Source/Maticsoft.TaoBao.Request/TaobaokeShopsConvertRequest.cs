namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TaobaokeShopsConvertRequest : ITopRequest<TaobaokeShopsConvertResponse>
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
            return "taobao.taobaoke.shops.convert";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("fields", this.Fields);
            dictionary.Add("is_mobile", this.IsMobile);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("outer_code", this.OuterCode);
            dictionary.Add("pid", this.Pid);
            dictionary.Add("seller_nicks", this.SellerNicks);
            dictionary.Add("sids", this.Sids);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxListSize("seller_nicks", this.SellerNicks, 10);
            RequestValidator.ValidateMaxListSize("sids", this.Sids, 10);
        }

        public string Fields { get; set; }

        public bool? IsMobile { get; set; }

        public string Nick { get; set; }

        public string OuterCode { get; set; }

        public long? Pid { get; set; }

        public string SellerNicks { get; set; }

        public string Sids { get; set; }
    }
}

