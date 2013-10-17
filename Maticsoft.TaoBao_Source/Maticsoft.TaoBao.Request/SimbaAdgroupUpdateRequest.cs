namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaAdgroupUpdateRequest : ITopRequest<SimbaAdgroupUpdateResponse>
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
            return "taobao.simba.adgroup.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_id", this.AdgroupId);
            dictionary.Add("default_price", this.DefaultPrice);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("nonsearch_max_price", this.NonsearchMaxPrice);
            dictionary.Add("online_status", this.OnlineStatus);
            dictionary.Add("use_nonsearch_default_price", this.UseNonsearchDefaultPrice);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("adgroup_id", this.AdgroupId);
            RequestValidator.ValidateMinValue("default_price", this.DefaultPrice, 5L);
            RequestValidator.ValidateMinValue("nonsearch_max_price", this.NonsearchMaxPrice, 5L);
        }

        public long? AdgroupId { get; set; }

        public long? DefaultPrice { get; set; }

        public string Nick { get; set; }

        public long? NonsearchMaxPrice { get; set; }

        public string OnlineStatus { get; set; }

        public bool? UseNonsearchDefaultPrice { get; set; }
    }
}

