namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TopatsTasksGetRequest : ITopRequest<TopatsTasksGetResponse>
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
            return "taobao.topats.tasks.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("start_time", this.StartTime);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("end_time", this.EndTime);
            RequestValidator.ValidateRequired("start_time", this.StartTime);
        }

        public DateTime? EndTime { get; set; }

        public DateTime? StartTime { get; set; }
    }
}

