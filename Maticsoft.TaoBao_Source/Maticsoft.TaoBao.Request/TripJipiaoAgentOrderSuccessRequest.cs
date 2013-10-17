namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TripJipiaoAgentOrderSuccessRequest : ITopRequest<TripJipiaoAgentOrderSuccessResponse>
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
            return "taobao.trip.jipiao.agent.order.success";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("order_id", this.OrderId);
            dictionary.Add("success_info", this.SuccessInfo);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("order_id", this.OrderId);
            RequestValidator.ValidateRequired("success_info", this.SuccessInfo);
            RequestValidator.ValidateMaxListSize("success_info", this.SuccessInfo, 9);
        }

        public long? OrderId { get; set; }

        public string SuccessInfo { get; set; }
    }
}

