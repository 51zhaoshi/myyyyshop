namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaCampaignUpdateRequest : ITopRequest<SimbaCampaignUpdateResponse>
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
            return "taobao.simba.campaign.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("online_status", this.OnlineStatus);
            dictionary.Add("title", this.Title);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
            RequestValidator.ValidateRequired("online_status", this.OnlineStatus);
            RequestValidator.ValidateRequired("title", this.Title);
            RequestValidator.ValidateMaxLength("title", this.Title, 20);
        }

        public long? CampaignId { get; set; }

        public string Nick { get; set; }

        public string OnlineStatus { get; set; }

        public string Title { get; set; }
    }
}

