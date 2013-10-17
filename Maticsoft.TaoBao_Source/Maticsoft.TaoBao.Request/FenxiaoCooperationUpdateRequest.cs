namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoCooperationUpdateRequest : ITopRequest<FenxiaoCooperationUpdateResponse>
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
            return "taobao.fenxiao.cooperation.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("distributor_id", this.DistributorId);
            dictionary.Add("grade_id", this.GradeId);
            dictionary.Add("trade_type", this.TradeType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("distributor_id", this.DistributorId);
            RequestValidator.ValidateRequired("grade_id", this.GradeId);
        }

        public long? DistributorId { get; set; }

        public long? GradeId { get; set; }

        public string TradeType { get; set; }
    }
}

