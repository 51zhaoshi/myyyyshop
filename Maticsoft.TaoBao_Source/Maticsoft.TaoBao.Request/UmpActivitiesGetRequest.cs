namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class UmpActivitiesGetRequest : ITopRequest<UmpActivitiesGetResponse>
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
            return "taobao.ump.activities.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("tool_id", this.ToolId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("page_no", this.PageNo);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 0L);
            RequestValidator.ValidateRequired("page_size", this.PageSize);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 50L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
            RequestValidator.ValidateRequired("tool_id", this.ToolId);
        }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public long? ToolId { get; set; }
    }
}

