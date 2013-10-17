namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LogisticsOrdersDetailGetRequest : ITopRequest<LogisticsOrdersDetailGetResponse>
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
            return "taobao.logistics.orders.detail.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("buyer_nick", this.BuyerNick);
            dictionary.Add("end_created", this.EndCreated);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("freight_payer", this.FreightPayer);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("receiver_name", this.ReceiverName);
            dictionary.Add("seller_confirm", this.SellerConfirm);
            dictionary.Add("start_created", this.StartCreated);
            dictionary.Add("status", this.Status);
            dictionary.Add("tid", this.Tid);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 100L);
        }

        public string BuyerNick { get; set; }

        public DateTime? EndCreated { get; set; }

        public string Fields { get; set; }

        public string FreightPayer { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public string ReceiverName { get; set; }

        public string SellerConfirm { get; set; }

        public DateTime? StartCreated { get; set; }

        public string Status { get; set; }

        public long? Tid { get; set; }

        public string Type { get; set; }
    }
}

