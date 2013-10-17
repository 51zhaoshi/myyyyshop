namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaNonsearchAdgroupplacesAddRequest : ITopRequest<SimbaNonsearchAdgroupplacesAddResponse>
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
            return "taobao.simba.nonsearch.adgroupplaces.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_places_json", this.AdgroupPlacesJson);
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("nick", this.Nick);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("adgroup_places_json", this.AdgroupPlacesJson);
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
        }

        public string AdgroupPlacesJson { get; set; }

        public long? CampaignId { get; set; }

        public string Nick { get; set; }
    }
}

