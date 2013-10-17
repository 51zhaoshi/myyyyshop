namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaAdgroupAddRequest : ITopRequest<SimbaAdgroupAddResponse>
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
            return "taobao.simba.adgroup.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("campaign_id", this.CampaignId);
            dictionary.Add("default_price", this.DefaultPrice);
            dictionary.Add("img_url", this.ImgUrl);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("title", this.Title);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("campaign_id", this.CampaignId);
            RequestValidator.ValidateRequired("default_price", this.DefaultPrice);
            RequestValidator.ValidateMinValue("default_price", this.DefaultPrice, 5L);
            RequestValidator.ValidateRequired("img_url", this.ImgUrl);
            RequestValidator.ValidateRequired("item_id", this.ItemId);
            RequestValidator.ValidateRequired("title", this.Title);
            RequestValidator.ValidateMaxLength("title", this.Title, 40);
        }

        public long? CampaignId { get; set; }

        public long? DefaultPrice { get; set; }

        public string ImgUrl { get; set; }

        public long? ItemId { get; set; }

        public string Nick { get; set; }

        public string Title { get; set; }
    }
}

