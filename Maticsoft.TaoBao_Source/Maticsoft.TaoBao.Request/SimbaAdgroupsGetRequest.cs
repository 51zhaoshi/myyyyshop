namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaAdgroupsGetRequest : ITopRequest<SimbaAdgroupsGetResponse>
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
            return "taobao.simba.adgroups.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_ids", this.AdgroupIds);
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxListSize("adgroup_ids", this.AdgroupIds, 200);
            RequestValidator.ValidateRequired("page_no", this.PageNo);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1L);
            RequestValidator.ValidateRequired("page_size", this.PageSize);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 200L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
        }

        public string AdgroupIds { get; set; }

        public long? CampaignId { get; set; }

        public string Nick { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }
    }
}

