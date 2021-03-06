namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WangwangEserviceEvalsGetRequest : ITopRequest<WangwangEserviceEvalsGetResponse>
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
            return "taobao.wangwang.eservice.evals.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_date", this.EndDate);
            dictionary.Add("service_staff_id", this.ServiceStaffId);
            dictionary.Add("start_date", this.StartDate);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("end_date", this.EndDate);
            RequestValidator.ValidateRequired("service_staff_id", this.ServiceStaffId);
            RequestValidator.ValidateMaxListSize("service_staff_id", this.ServiceStaffId, 30);
            RequestValidator.ValidateRequired("start_date", this.StartDate);
        }

        public DateTime? EndDate { get; set; }

        public string ServiceStaffId { get; set; }

        public DateTime? StartDate { get; set; }
    }
}

