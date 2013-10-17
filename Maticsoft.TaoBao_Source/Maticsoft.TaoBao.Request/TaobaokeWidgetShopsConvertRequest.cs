namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TaobaokeWidgetShopsConvertRequest : ITopRequest<TaobaokeWidgetShopsConvertResponse>
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
            return "taobao.taobaoke.widget.shops.convert";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("fields", this.Fields);
            dictionary.Add("is_mobile", this.IsMobile);
            dictionary.Add("outer_code", this.OuterCode);
            dictionary.Add("seller_nicks", this.SellerNicks);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("seller_nicks", this.SellerNicks);
            RequestValidator.ValidateMaxListSize("seller_nicks", this.SellerNicks, 10);
        }

        public string Fields { get; set; }

        public bool? IsMobile { get; set; }

        public string OuterCode { get; set; }

        public string SellerNicks { get; set; }
    }
}

