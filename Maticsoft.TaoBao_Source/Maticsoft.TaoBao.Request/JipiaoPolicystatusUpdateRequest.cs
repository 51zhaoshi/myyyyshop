namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JipiaoPolicystatusUpdateRequest : ITopRequest<JipiaoPolicystatusUpdateResponse>
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
            return "taobao.jipiao.policystatus.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("policy_id", this.PolicyId);
            dictionary.Add("status", this.Status);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("policy_id", this.PolicyId);
            RequestValidator.ValidateMaxListSize("policy_id", this.PolicyId, 100);
            RequestValidator.ValidateMaxLength("policy_id", this.PolicyId, 0x1964);
            RequestValidator.ValidateRequired("status", this.Status);
            RequestValidator.ValidateRequired("type", this.Type);
            RequestValidator.ValidateMaxValue("type", this.Type, 1L);
            RequestValidator.ValidateMinValue("type", this.Type, 0L);
        }

        public string PolicyId { get; set; }

        public long? Status { get; set; }

        public long? Type { get; set; }
    }
}

