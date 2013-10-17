namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemConsignmentPageGetRequest : ITopRequest<WlbItemConsignmentPageGetResponse>
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
            return "taobao.wlb.item.consignment.page.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("ic_item_id", this.IcItemId);
            dictionary.Add("owner_item_id", this.OwnerItemId);
            dictionary.Add("owner_user_nick", this.OwnerUserNick);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public long? IcItemId { get; set; }

        public long? OwnerItemId { get; set; }

        public string OwnerUserNick { get; set; }
    }
}

