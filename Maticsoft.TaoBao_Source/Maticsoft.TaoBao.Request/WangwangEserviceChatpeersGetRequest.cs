namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WangwangEserviceChatpeersGetRequest : ITopRequest<WangwangEserviceChatpeersGetResponse>
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
            return "taobao.wangwang.eservice.chatpeers.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("charset", this.Charset);
            dictionary.Add("chat_id", this.ChatId);
            dictionary.Add("end_date", this.EndDate);
            dictionary.Add("start_date", this.StartDate);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("chat_id", this.ChatId);
            RequestValidator.ValidateRequired("end_date", this.EndDate);
            RequestValidator.ValidateRequired("start_date", this.StartDate);
        }

        public string Charset { get; set; }

        public string ChatId { get; set; }

        public string EndDate { get; set; }

        public string StartDate { get; set; }
    }
}

