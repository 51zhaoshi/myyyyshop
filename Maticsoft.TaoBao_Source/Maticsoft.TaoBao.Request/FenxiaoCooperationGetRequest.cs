namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoCooperationGetRequest : ITopRequest<FenxiaoCooperationGetResponse>
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
            return "taobao.fenxiao.cooperation.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("end_date", this.EndDate);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("start_date", this.StartDate);
            dictionary.Add("status", this.Status);
            dictionary.Add("trade_type", this.TradeType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public DateTime? EndDate { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public DateTime? StartDate { get; set; }

        public string Status { get; set; }

        public string TradeType { get; set; }
    }
}

