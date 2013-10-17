namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AlibabaLogisticsOrderChargeRequest : ITopRequest<AlibabaLogisticsOrderChargeResponse>
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
            return "alibaba.logistics.order.charge";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cargo_description", this.CargoDescription);
            dictionary.Add("cargo_name", this.CargoName);
            dictionary.Add("pay_type", this.PayType);
            dictionary.Add("route_code", this.RouteCode);
            dictionary.Add("total_number", this.TotalNumber);
            dictionary.Add("total_volume", this.TotalVolume);
            dictionary.Add("total_weight", this.TotalWeight);
            dictionary.Add("vas_list", this.VasList);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cargo_name", this.CargoName);
            RequestValidator.ValidateRequired("pay_type", this.PayType);
            RequestValidator.ValidateRequired("route_code", this.RouteCode);
            RequestValidator.ValidateRequired("total_number", this.TotalNumber);
            RequestValidator.ValidateMaxListSize("vas_list", this.VasList, 12);
        }

        public string CargoDescription { get; set; }

        public string CargoName { get; set; }

        public string PayType { get; set; }

        public string RouteCode { get; set; }

        public long? TotalNumber { get; set; }

        public string TotalVolume { get; set; }

        public string TotalWeight { get; set; }

        public string VasList { get; set; }
    }
}

