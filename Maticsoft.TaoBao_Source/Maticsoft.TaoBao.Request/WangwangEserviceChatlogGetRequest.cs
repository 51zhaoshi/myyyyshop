namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WangwangEserviceChatlogGetRequest : ITopRequest<WangwangEserviceChatlogGetResponse>
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
            return "taobao.wangwang.eservice.chatlog.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_date", this.EndDate);
            dictionary.Add("from_id", this.FromId);
            dictionary.Add("start_date", this.StartDate);
            dictionary.Add("to_id", this.ToId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("end_date", this.EndDate);
            RequestValidator.ValidateRequired("from_id", this.FromId);
            RequestValidator.ValidateRequired("start_date", this.StartDate);
            RequestValidator.ValidateRequired("to_id", this.ToId);
        }

        public string EndDate { get; set; }

        public string FromId { get; set; }

        public string StartDate { get; set; }

        public string ToId { get; set; }
    }
}

