namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LogisticsOrderstorePushRequest : ITopRequest<LogisticsOrderstorePushResponse>
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
            return "taobao.logistics.orderstore.push";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("occure_time", this.OccureTime);
            dictionary.Add("operate_detail", this.OperateDetail);
            dictionary.Add("operator_contact", this.OperatorContact);
            dictionary.Add("operator_name", this.OperatorName);
            dictionary.Add("trade_id", this.TradeId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("occure_time", this.OccureTime);
            RequestValidator.ValidateRequired("operate_detail", this.OperateDetail);
            RequestValidator.ValidateMaxLength("operate_detail", this.OperateDetail, 200);
            RequestValidator.ValidateMaxLength("operator_contact", this.OperatorContact, 20);
            RequestValidator.ValidateMaxLength("operator_name", this.OperatorName, 20);
            RequestValidator.ValidateRequired("trade_id", this.TradeId);
        }

        public DateTime? OccureTime { get; set; }

        public string OperateDetail { get; set; }

        public string OperatorContact { get; set; }

        public string OperatorName { get; set; }

        public long? TradeId { get; set; }
    }
}

