namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoTrademonitorGetRequest : ITopRequest<FenxiaoTrademonitorGetResponse>
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
            return "taobao.fenxiao.trademonitor.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("distributor_nick", this.DistributorNick);
            dictionary.Add("end_created", this.EndCreated);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("start_created", this.StartCreated);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public string DistributorNick { get; set; }

        public DateTime? EndCreated { get; set; }

        public string Fields { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public long? ProductId { get; set; }

        public DateTime? StartCreated { get; set; }
    }
}

