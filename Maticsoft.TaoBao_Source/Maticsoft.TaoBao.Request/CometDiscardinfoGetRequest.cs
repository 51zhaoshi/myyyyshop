namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CometDiscardinfoGetRequest : ITopRequest<CometDiscardinfoGetResponse>
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
            return "taobao.comet.discardinfo.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end", this.End);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("start", this.Start);
            dictionary.Add("types", this.Types);
            dictionary.Add("user_id", this.UserId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("start", this.Start);
        }

        public DateTime? End { get; set; }

        public string Nick { get; set; }

        public DateTime? Start { get; set; }

        public string Types { get; set; }

        public long? UserId { get; set; }
    }
}

