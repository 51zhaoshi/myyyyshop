namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SellercatsListAddRequest : ITopRequest<SellercatsListAddResponse>
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
            return "taobao.sellercats.list.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("name", this.Name);
            dictionary.Add("parent_cid", this.ParentCid);
            dictionary.Add("pict_url", this.PictUrl);
            dictionary.Add("sort_order", this.SortOrder);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("name", this.Name);
        }

        public string Name { get; set; }

        public long? ParentCid { get; set; }

        public string PictUrl { get; set; }

        public long? SortOrder { get; set; }
    }
}

