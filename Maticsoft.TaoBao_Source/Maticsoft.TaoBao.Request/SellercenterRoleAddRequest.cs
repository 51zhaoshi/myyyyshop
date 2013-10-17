namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SellercenterRoleAddRequest : ITopRequest<SellercenterRoleAddResponse>
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
            return "taobao.sellercenter.role.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("description", this.Description);
            dictionary.Add("name", this.Name);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("permission_codes", this.PermissionCodes);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("description", this.Description, 20);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 20);
            RequestValidator.ValidateRequired("nick", this.Nick);
            RequestValidator.ValidateMaxLength("nick", this.Nick, 500);
            RequestValidator.ValidateMaxListSize("permission_codes", this.PermissionCodes, 0x7d0);
        }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Nick { get; set; }

        public string PermissionCodes { get; set; }
    }
}

