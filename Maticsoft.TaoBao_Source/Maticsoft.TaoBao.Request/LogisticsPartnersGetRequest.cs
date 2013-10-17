namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LogisticsPartnersGetRequest : ITopRequest<LogisticsPartnersGetResponse>
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
            return "taobao.logistics.partners.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("goods_value", this.GoodsValue);
            dictionary.Add("is_need_carriage", this.IsNeedCarriage);
            dictionary.Add("service_type", this.ServiceType);
            dictionary.Add("source_id", this.SourceId);
            dictionary.Add("target_id", this.TargetId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("service_type", this.ServiceType);
        }

        public string GoodsValue { get; set; }

        public bool? IsNeedCarriage { get; set; }

        public string ServiceType { get; set; }

        public string SourceId { get; set; }

        public string TargetId { get; set; }
    }
}

