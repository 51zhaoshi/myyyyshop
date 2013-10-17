namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TopatsIncrementMessagesGetRequest : ITopRequest<TopatsIncrementMessagesGetResponse>
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
            return "taobao.topats.increment.messages.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end", this.End);
            dictionary.Add("start", this.Start);
            dictionary.Add("topics", this.Topics);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("end", this.End);
            RequestValidator.ValidateRequired("start", this.Start);
            RequestValidator.ValidateRequired("topics", this.Topics);
        }

        public DateTime? End { get; set; }

        public DateTime? Start { get; set; }

        public string Topics { get; set; }
    }
}

