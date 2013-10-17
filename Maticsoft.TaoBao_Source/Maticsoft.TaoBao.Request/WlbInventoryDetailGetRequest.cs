namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbInventoryDetailGetRequest : ITopRequest<WlbInventoryDetailGetResponse>
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
            return "taobao.wlb.inventory.detail.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("inventory_type_list", this.InventoryTypeList);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("store_code", this.StoreCode);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxListSize("inventory_type_list", this.InventoryTypeList, 20);
            RequestValidator.ValidateRequired("item_id", this.ItemId);
        }

        public string InventoryTypeList { get; set; }

        public long? ItemId { get; set; }

        public string StoreCode { get; set; }
    }
}

