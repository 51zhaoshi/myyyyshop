namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SkusQuantityUpdateRequest : ITopRequest<SkusQuantityUpdateResponse>
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
            return "taobao.skus.quantity.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("outerid_quantities", this.OuteridQuantities);
            dictionary.Add("skuid_quantities", this.SkuidQuantities);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
        }

        public long? NumIid { get; set; }

        public string OuteridQuantities { get; set; }

        public string SkuidQuantities { get; set; }

        public long? Type { get; set; }
    }
}

