namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LogisticsOfflineSendRequest : ITopRequest<LogisticsOfflineSendResponse>
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
            return "taobao.logistics.offline.send";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cancel_id", this.CancelId);
            dictionary.Add("company_code", this.CompanyCode);
            dictionary.Add("feature", this.Feature);
            dictionary.Add("out_sid", this.OutSid);
            dictionary.Add("sender_id", this.SenderId);
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

        public long? CancelId { get; set; }

        public string CompanyCode { get; set; }

        public string Feature { get; set; }

        public string OutSid { get; set; }

        public long? SenderId { get; set; }

        public long? Tid { get; set; }
    }
}

