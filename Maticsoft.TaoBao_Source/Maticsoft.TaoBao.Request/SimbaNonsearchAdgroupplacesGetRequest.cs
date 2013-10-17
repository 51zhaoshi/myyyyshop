namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaNonsearchAdgroupplacesGetRequest : ITopRequest<SimbaNonsearchAdgroupplacesGetResponse>
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
            return "taobao.simba.nonsearch.adgroupplaces.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_ids", this.AdgroupIds);
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("nick", this.Nick);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("adgroup_ids", this.AdgroupIds);
            RequestValidator.ValidateMaxListSize("adgroup_ids", this.AdgroupIds, 200);
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
        }

        public string AdgroupIds { get; set; }

        public long? CampaignId { get; set; }

        public string Nick { get; set; }
    }
}

