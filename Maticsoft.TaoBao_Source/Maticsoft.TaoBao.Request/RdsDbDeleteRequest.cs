namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RdsDbDeleteRequest : ITopRequest<RdsDbDeleteResponse>
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
            return "taobao.rds.db.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("db_id", this.DbId);
            dictionary.Add("instance_name", this.InstanceName);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("db_id", this.DbId);
            RequestValidator.ValidateRequired("instance_name", this.InstanceName);
            RequestValidator.ValidateMaxLength("instance_name", this.InstanceName, 30);
        }

        public long? DbId { get; set; }

        public string InstanceName { get; set; }
    }
}

