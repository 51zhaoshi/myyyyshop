namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbOrderPageGetRequest : ITopRequest<WlbOrderPageGetResponse>
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
            return "taobao.wlb.order.page.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("order_code", this.OrderCode);
            dictionary.Add("order_status", this.OrderStatus);
            dictionary.Add("order_sub_type", this.OrderSubType);
            dictionary.Add("order_type", this.OrderType);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("start_time", this.StartTime);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public DateTime? EndTime { get; set; }

        public string OrderCode { get; set; }

        public long? OrderStatus { get; set; }

        public string OrderSubType { get; set; }

        public string OrderType { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public DateTime? StartTime { get; set; }
    }
}

