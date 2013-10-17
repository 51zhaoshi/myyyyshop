namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemAnchorGetRequest : ITopRequest<ItemAnchorGetResponse>
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
            return "taobao.item.anchor.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cat_id", this.CatId);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cat_id", this.CatId);
            RequestValidator.ValidateRequired("type", this.Type);
            RequestValidator.ValidateMaxValue("type", this.Type, 1L);
            RequestValidator.ValidateMinValue("type", this.Type, -1L);
        }

        public long? CatId { get; set; }

        public long? Type { get; set; }
    }
}

