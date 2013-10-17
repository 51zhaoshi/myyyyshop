namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LogisticsAddressModifyRequest : ITopRequest<LogisticsAddressModifyResponse>
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
            return "taobao.logistics.address.modify";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("addr", this.Addr);
            dictionary.Add("cancel_def", this.CancelDef);
            dictionary.Add("city", this.City);
            dictionary.Add("contact_id", this.ContactId);
            dictionary.Add("contact_name", this.ContactName);
            dictionary.Add("country", this.Country);
            dictionary.Add("get_def", this.GetDef);
            dictionary.Add("memo", this.Memo);
            dictionary.Add("mobile_phone", this.MobilePhone);
            dictionary.Add("phone", this.Phone);
            dictionary.Add("province", this.Province);
            dictionary.Add("seller_company", this.SellerCompany);
            dictionary.Add("zip_code", this.ZipCode);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("addr", this.Addr);
            RequestValidator.ValidateRequired("city", this.City);
            RequestValidator.ValidateRequired("contact_id", this.ContactId);
            RequestValidator.ValidateRequired("contact_name", this.ContactName);
            RequestValidator.ValidateRequired("province", this.Province);
        }

        public string Addr { get; set; }

        public bool? CancelDef { get; set; }

        public string City { get; set; }

        public long? ContactId { get; set; }

        public string ContactName { get; set; }

        public string Country { get; set; }

        public bool? GetDef { get; set; }

        public string Memo { get; set; }

        public string MobilePhone { get; set; }

        public string Phone { get; set; }

        public string Province { get; set; }

        public string SellerCompany { get; set; }

        public string ZipCode { get; set; }
    }
}

