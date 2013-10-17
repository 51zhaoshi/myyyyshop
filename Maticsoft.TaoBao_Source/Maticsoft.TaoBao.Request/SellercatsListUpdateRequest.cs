namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SellercatsListUpdateRequest : ITopRequest<SellercatsListUpdateResponse>
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
            return "taobao.sellercats.list.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cid", this.Cid);
            dictionary.Add("name", this.Name);
            dictionary.Add("pict_url", this.PictUrl);
            dictionary.Add("sort_order", this.SortOrder);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cid", this.Cid);
        }

        public long? Cid { get; set; }

        public string Name { get; set; }

        public string PictUrl { get; set; }

        public long? SortOrder { get; set; }
    }
}

