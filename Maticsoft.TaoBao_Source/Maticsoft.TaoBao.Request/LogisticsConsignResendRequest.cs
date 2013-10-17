namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LogisticsConsignResendRequest : ITopRequest<LogisticsConsignResendResponse>
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
            return "taobao.logistics.consign.resend";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("company_code", this.CompanyCode);
            dictionary.Add("feature", this.Feature);
            dictionary.Add("out_sid", this.OutSid);
            dictionary.Add("tid", this.Tid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("company_code", this.CompanyCode);
            RequestValidator.ValidateRequired("out_sid", this.OutSid);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        public string CompanyCode { get; set; }

        public string Feature { get; set; }

        public string OutSid { get; set; }

        public long? Tid { get; set; }
    }
}

