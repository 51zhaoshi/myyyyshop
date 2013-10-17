namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaCreativeAddRequest : ITopRequest<SimbaCreativeAddResponse>
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
            return "taobao.simba.creative.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_id", this.AdgroupId);
            dictionary.Add("img_url", this.ImgUrl);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("title", this.Title);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("adgroup_id", this.AdgroupId);
            RequestValidator.ValidateRequired("img_url", this.ImgUrl);
            RequestValidator.ValidateRequired("title", this.Title);
            RequestValidator.ValidateMaxLength("title", this.Title, 40);
        }

        public long? AdgroupId { get; set; }

        public string ImgUrl { get; set; }

        public string Nick { get; set; }

        public string Title { get; set; }
    }
}

