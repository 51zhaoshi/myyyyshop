namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class UmpRangeDeleteRequest : ITopRequest<UmpRangeDeleteResponse>
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
            return "taobao.ump.range.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("act_id", this.ActId);
            dictionary.Add("ids", this.Ids);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("act_id", this.ActId);
            RequestValidator.ValidateRequired("ids", this.Ids);
            RequestValidator.ValidateRequired("type", this.Type);
        }

        public long? ActId { get; set; }

        public string Ids { get; set; }

        public long? Type { get; set; }
    }
}

