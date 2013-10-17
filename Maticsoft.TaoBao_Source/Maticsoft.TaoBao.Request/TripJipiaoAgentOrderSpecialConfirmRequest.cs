namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TripJipiaoAgentOrderSpecialConfirmRequest : ITopRequest<TripJipiaoAgentOrderSpecialConfirmResponse>
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
            return "taobao.trip.jipiao.agent.order.special.confirm";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("can_pay", this.CanPay);
            dictionary.Add("fail_memo", this.FailMemo);
            dictionary.Add("fail_type", this.FailType);
            dictionary.Add("order_id", this.OrderId);
            dictionary.Add("pay_latest_time", this.PayLatestTime);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("can_pay", this.CanPay);
            RequestValidator.ValidateMaxLength("fail_memo", this.FailMemo, 200);
            RequestValidator.ValidateRequired("order_id", this.OrderId);
        }

        public bool? CanPay { get; set; }

        public string FailMemo { get; set; }

        public long? FailType { get; set; }

        public long? OrderId { get; set; }

        public DateTime? PayLatestTime { get; set; }
    }
}

