namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaCampaignPlatformUpdateRequest : ITopRequest<SimbaCampaignPlatformUpdateResponse>
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
            return "taobao.simba.campaign.platform.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("nonsearch_channels", this.NonsearchChannels);
            dictionary.Add("outside_discount", this.OutsideDiscount);
            dictionary.Add("search_channels", this.SearchChannels);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
            RequestValidator.ValidateMaxListSize("nonsearch_channels", this.NonsearchChannels, 10);
            RequestValidator.ValidateRequired("outside_discount", this.OutsideDiscount);
            RequestValidator.ValidateMaxValue("outside_discount", this.OutsideDiscount, 200L);
            RequestValidator.ValidateMinValue("outside_discount", this.OutsideDiscount, 1L);
            RequestValidator.ValidateRequired("search_channels", this.SearchChannels);
            RequestValidator.ValidateMaxListSize("search_channels", this.SearchChannels, 10);
        }

        public long? CampaignId { get; set; }

        public string Nick { get; set; }

        public string NonsearchChannels { get; set; }

        public long? OutsideDiscount { get; set; }

        public string SearchChannels { get; set; }
    }
}

