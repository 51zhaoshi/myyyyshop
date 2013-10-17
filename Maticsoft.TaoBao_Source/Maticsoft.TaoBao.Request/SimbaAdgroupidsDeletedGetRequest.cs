namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimbaAdgroupidsDeletedGetRequest : ITopRequest<SimbaAdgroupidsDeletedGetResponse>
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
            return "taobao.simba.adgroupids.deleted.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("nick", this.Nick);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("start_time", this.StartTime);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1L);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 0x3e8L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
            RequestValidator.ValidateRequired("start_time", this.StartTime);
        }

        public string Nick { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public DateTime? StartTime { get; set; }
    }
}

