namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TripJipiaoAgentOrderHkRequest : ITopRequest<TripJipiaoAgentOrderHkResponse>
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
            return "taobao.trip.jipiao.agent.order.hk";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("order_id", this.OrderId);
            dictionary.Add("pnr_info", this.PnrInfo);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("order_id", this.OrderId);
            RequestValidator.ValidateRequired("pnr_info", this.PnrInfo);
            RequestValidator.ValidateMaxListSize("pnr_info", this.PnrInfo, 9);
        }

        public long? OrderId { get; set; }

        public string PnrInfo { get; set; }
    }
}

