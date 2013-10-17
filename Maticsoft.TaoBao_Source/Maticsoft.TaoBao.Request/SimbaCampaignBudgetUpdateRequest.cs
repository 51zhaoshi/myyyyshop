namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaCampaignBudgetUpdateRequest : ITopRequest<SimbaCampaignBudgetUpdateResponse>
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
            return "taobao.simba.campaign.budget.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("budget", this.Budget);
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("use_smooth", this.UseSmooth);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxValue("budget", this.Budget, 0x1869fL);
            RequestValidator.ValidateMinValue("budget", this.Budget, 30L);
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
            RequestValidator.ValidateRequired("use_smooth", this.UseSmooth);
        }

        public long? Budget { get; set; }

        public long? CampaignId { get; set; }

        public string Nick { get; set; }

        public bool? UseSmooth { get; set; }
    }
}

