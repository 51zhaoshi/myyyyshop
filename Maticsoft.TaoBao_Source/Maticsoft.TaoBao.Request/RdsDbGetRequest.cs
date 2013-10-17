namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RdsDbGetRequest : ITopRequest<RdsDbGetResponse>
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
            return "taobao.rds.db.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("db_status", this.DbStatus);
            dictionary.Add("instance_name", this.InstanceName);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxValue("db_status", this.DbStatus, 3L);
            RequestValidator.ValidateMinValue("db_status", this.DbStatus, 0L);
            RequestValidator.ValidateRequired("instance_name", this.InstanceName);
            RequestValidator.ValidateMaxLength("instance_name", this.InstanceName, 30);
        }

        public long? DbStatus { get; set; }

        public string InstanceName { get; set; }
    }
}

