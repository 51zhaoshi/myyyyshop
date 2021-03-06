namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RdsDbCreateRequest : ITopRequest<RdsDbCreateResponse>
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
            return "taobao.rds.db.create";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("db_name", this.DbName);
            dictionary.Add("instance_name", this.InstanceName);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("db_name", this.DbName);
            RequestValidator.ValidateMaxLength("db_name", this.DbName, 0x40);
            RequestValidator.ValidateRequired("instance_name", this.InstanceName);
            RequestValidator.ValidateMaxLength("instance_name", this.InstanceName, 30);
        }

        public string DbName { get; set; }

        public string InstanceName { get; set; }
    }
}

