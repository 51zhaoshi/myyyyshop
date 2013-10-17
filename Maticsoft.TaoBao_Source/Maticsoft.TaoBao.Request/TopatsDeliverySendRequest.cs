namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TopatsDeliverySendRequest : ITopRequest<TopatsDeliverySendResponse>
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
            return "taobao.topats.delivery.send";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("company_codes", this.CompanyCodes);
            dictionary.Add("memos", this.Memos);
            dictionary.Add("order_types", this.OrderTypes);
            dictionary.Add("out_sids", this.OutSids);
            dictionary.Add("seller_address", this.SellerAddress);
            dictionary.Add("seller_area_id", this.SellerAreaId);
            dictionary.Add("seller_mobile", this.SellerMobile);
            dictionary.Add("seller_name", this.SellerName);
            dictionary.Add("seller_phone", this.SellerPhone);
            dictionary.Add("seller_zip", this.SellerZip);
            dictionary.Add("tids", this.Tids);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("tids", this.Tids);
        }

        public string CompanyCodes { get; set; }

        public string Memos { get; set; }

        public string OrderTypes { get; set; }

        public string OutSids { get; set; }

        public string SellerAddress { get; set; }

        public long? SellerAreaId { get; set; }

        public string SellerMobile { get; set; }

        public string SellerName { get; set; }

        public string SellerPhone { get; set; }

        public string SellerZip { get; set; }

        public string Tids { get; set; }
    }
}

