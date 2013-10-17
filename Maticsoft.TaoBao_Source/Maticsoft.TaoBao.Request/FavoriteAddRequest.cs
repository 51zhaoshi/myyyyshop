namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FavoriteAddRequest : ITopRequest<FavoriteAddResponse>
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
            return "taobao.favorite.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("collect_type", this.CollectType);
            dictionary.Add("item_numid", this.ItemNumid);
            dictionary.Add("shared", this.Shared);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("collect_type", this.CollectType);
            RequestValidator.ValidateRequired("item_numid", this.ItemNumid);
            RequestValidator.ValidateMinValue("item_numid", this.ItemNumid, 1L);
        }

        public string CollectType { get; set; }

        public long? ItemNumid { get; set; }

        public bool? Shared { get; set; }
    }
}

