namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaRptAdgroupkeywordbaseGetRequest : ITopRequest<SimbaRptAdgroupkeywordbaseGetResponse>
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
            return "taobao.simba.rpt.adgroupkeywordbase.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_id", this.AdgroupId);
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("search_type", this.SearchType);
            dictionary.Add("source", this.Source);
            dictionary.Add("start_time", this.StartTime);
            dictionary.Add("subway_token", this.SubwayToken);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("adgroup_id", this.AdgroupId);
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
            RequestValidator.ValidateRequired("end_time", this.EndTime);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
            RequestValidator.ValidateRequired("search_type", this.SearchType);
            RequestValidator.ValidateRequired("start_time", this.StartTime);
            RequestValidator.ValidateRequired("subway_token", this.SubwayToken);
        }

        public long? AdgroupId { get; set; }

        public long? CampaignId { get; set; }

        public string EndTime { get; set; }

        public string Nick { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string SearchType { get; set; }

        public string Source { get; set; }

        public string StartTime { get; set; }

        public string SubwayToken { get; set; }
    }
}

