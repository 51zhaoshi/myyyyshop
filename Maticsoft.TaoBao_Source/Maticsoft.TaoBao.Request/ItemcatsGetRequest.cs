namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemcatsGetRequest : ITopRequest<ItemcatsGetResponse>
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
            return "taobao.itemcats.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cids", this.Cids);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("parent_cid", this.ParentCid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxListSize("cids", this.Cids, 0x3e8);
            RequestValidator.ValidateMaxValue("parent_cid", this.ParentCid, 0x7fffffffffffffffL);
            RequestValidator.ValidateMinValue("parent_cid", this.ParentCid, 0L);
        }

        public string Cids { get; set; }

        public string Fields { get; set; }

        public long? ParentCid { get; set; }
    }
}

