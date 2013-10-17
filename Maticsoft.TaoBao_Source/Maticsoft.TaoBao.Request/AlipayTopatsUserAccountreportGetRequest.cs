namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AlipayTopatsUserAccountreportGetRequest : ITopRequest<AlipayTopatsUserAccountreportGetResponse>
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
            return "alipay.topats.user.accountreport.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("charset", this.Charset);
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("start_time", this.StartTime);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("end_time", this.EndTime);
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("start_time", this.StartTime);
        }

        public string Charset { get; set; }

        public DateTime? EndTime { get; set; }

        public string Fields { get; set; }

        public DateTime? StartTime { get; set; }

        public string Type { get; set; }
    }
}

