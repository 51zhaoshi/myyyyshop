namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemPropimgDeleteRequest : ITopRequest<ItemPropimgDeleteResponse>
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
            return "taobao.item.propimg.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("id", this.Id);
            dictionary.Add("num_iid", this.NumIid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("id", this.Id);
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 0L);
        }

        public long? Id { get; set; }

        public long? NumIid { get; set; }
    }
}

