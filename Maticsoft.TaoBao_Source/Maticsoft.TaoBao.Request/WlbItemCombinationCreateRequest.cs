namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemCombinationCreateRequest : ITopRequest<WlbItemCombinationCreateResponse>
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
            return "taobao.wlb.item.combination.create";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("dest_item_list", this.DestItemList);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("proportion_list", this.ProportionList);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("dest_item_list", this.DestItemList);
            RequestValidator.ValidateRequired("item_id", this.ItemId);
        }

        public string DestItemList { get; set; }

        public long? ItemId { get; set; }

        public string ProportionList { get; set; }
    }
}

