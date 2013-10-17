namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TopatsSimbaCampkeywordbaseGetRequest : ITopRequest<TopatsSimbaCampkeywordbaseGetResponse>
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
            return "taobao.topats.simba.campkeywordbase.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("search_type", this.SearchType);
            dictionary.Add("source", this.Source);
            dictionary.Add("time_slot", this.TimeSlot);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
            RequestValidator.ValidateRequired("search_type", this.SearchType);
            RequestValidator.ValidateRequired("source", this.Source);
            RequestValidator.ValidateRequired("time_slot", this.TimeSlot);
        }

        public long? CampaignId { get; set; }

        public string Nick { get; set; }

        public string SearchType { get; set; }

        public string Source { get; set; }

        public string TimeSlot { get; set; }
    }
}

