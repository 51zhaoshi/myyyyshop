namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaAdgroupCatmatchUpdateRequest : ITopRequest<SimbaAdgroupCatmatchUpdateResponse>
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
            return "taobao.simba.adgroup.catmatch.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_id", this.AdgroupId);
            dictionary.Add("catmatch_id", this.CatmatchId);
            dictionary.Add("max_price", this.MaxPrice);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("online_status", this.OnlineStatus);
            dictionary.Add("use_default_price", this.UseDefaultPrice);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("adgroup_id", this.AdgroupId);
            RequestValidator.ValidateRequired("catmatch_id", this.CatmatchId);
            RequestValidator.ValidateRequired("max_price", this.MaxPrice);
            RequestValidator.ValidateMinValue("max_price", this.MaxPrice, 5L);
            RequestValidator.ValidateRequired("online_status", this.OnlineStatus);
            RequestValidator.ValidateRequired("use_default_price", this.UseDefaultPrice);
        }

        public long? AdgroupId { get; set; }

        public long? CatmatchId { get; set; }

        public long? MaxPrice { get; set; }

        public string Nick { get; set; }

        public string OnlineStatus { get; set; }

        public bool? UseDefaultPrice { get; set; }
    }
}

