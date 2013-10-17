namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaKeywordsDeleteRequest : ITopRequest<SimbaKeywordsDeleteResponse>
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
            return "taobao.simba.keywords.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("keyword_ids", this.KeywordIds);
            dictionary.Add("nick", this.Nick);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
            RequestValidator.ValidateRequired("keyword_ids", this.KeywordIds);
            RequestValidator.ValidateMaxListSize("keyword_ids", this.KeywordIds, 100);
        }

        public long? CampaignId { get; set; }

        public string KeywordIds { get; set; }

        public string Nick { get; set; }
    }
}

