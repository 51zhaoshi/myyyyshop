namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TaobaokeCaturlGetRequest : ITopRequest<TaobaokeCaturlGetResponse>
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
            return "taobao.taobaoke.caturl.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cid", this.Cid);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("outer_code", this.OuterCode);
            dictionary.Add("pid", this.Pid);
            dictionary.Add("q", this.Q);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cid", this.Cid);
            RequestValidator.ValidateMinValue("cid", this.Cid, 0L);
        }

        public long? Cid { get; set; }

        public string Nick { get; set; }

        public string OuterCode { get; set; }

        public long? Pid { get; set; }

        public string Q { get; set; }
    }
}

