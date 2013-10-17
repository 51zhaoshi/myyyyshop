namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LogisticsOrdertracePushRequest : ITopRequest<LogisticsOrdertracePushResponse>
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
            return "taobao.logistics.ordertrace.push";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("company_name", this.CompanyName);
            dictionary.Add("current_city", this.CurrentCity);
            dictionary.Add("facility_name", this.FacilityName);
            dictionary.Add("mail_no", this.MailNo);
            dictionary.Add("next_city", this.NextCity);
            dictionary.Add("node_description", this.NodeDescription);
            dictionary.Add("occure_time", this.OccureTime);
            dictionary.Add("operate_detail", this.OperateDetail);
            dictionary.Add("operator_contact", this.OperatorContact);
            dictionary.Add("operator_name", this.OperatorName);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("company_name", this.CompanyName);
            RequestValidator.ValidateMaxLength("company_name", this.CompanyName, 20);
            RequestValidator.ValidateMaxLength("current_city", this.CurrentCity, 20);
            RequestValidator.ValidateMaxLength("facility_name", this.FacilityName, 100);
            RequestValidator.ValidateRequired("mail_no", this.MailNo);
            RequestValidator.ValidateMaxLength("next_city", this.NextCity, 20);
            RequestValidator.ValidateMaxLength("node_description", this.NodeDescription, 20);
            RequestValidator.ValidateRequired("occure_time", this.OccureTime);
            RequestValidator.ValidateRequired("operate_detail", this.OperateDetail);
            RequestValidator.ValidateMaxLength("operate_detail", this.OperateDetail, 200);
            RequestValidator.ValidateMaxLength("operator_contact", this.OperatorContact, 20);
            RequestValidator.ValidateMaxLength("operator_name", this.OperatorName, 20);
        }

        public string CompanyName { get; set; }

        public string CurrentCity { get; set; }

        public string FacilityName { get; set; }

        public string MailNo { get; set; }

        public string NextCity { get; set; }

        public string NodeDescription { get; set; }

        public DateTime? OccureTime { get; set; }

        public string OperateDetail { get; set; }

        public string OperatorContact { get; set; }

        public string OperatorName { get; set; }
    }
}

