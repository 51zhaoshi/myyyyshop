namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaAdgroupAdgroupcatmatchsGetRequest : ITopRequest<SimbaAdgroupAdgroupcatmatchsGetResponse>
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
            return "taobao.simba.adgroup.adgroupcatmatchs.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("adgroup_ids", this.AdgroupIds);
            dictionary.Add("nick", this.Nick);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("adgroup_ids", this.AdgroupIds);
            RequestValidator.ValidateMaxListSize("adgroup_ids", this.AdgroupIds, 200);
        }

        public string AdgroupIds { get; set; }

        public string Nick { get; set; }
    }
}

