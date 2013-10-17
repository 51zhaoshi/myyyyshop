namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelOrderFaceDealRequest : ITopRequest<HotelOrderFaceDealResponse>
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
            return "taobao.hotel.order.face.deal";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("oid", this.Oid);
            dictionary.Add("oper_type", this.OperType);
            dictionary.Add("reason_text", this.ReasonText);
            dictionary.Add("reason_type", this.ReasonType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("oid", this.Oid);
            RequestValidator.ValidateRequired("oper_type", this.OperType);
            RequestValidator.ValidateMaxLength("oper_type", this.OperType, 1);
            RequestValidator.ValidateMaxLength("reason_text", this.ReasonText, 500);
            RequestValidator.ValidateMaxLength("reason_type", this.ReasonType, 1);
        }

        public long? Oid { get; set; }

        public string OperType { get; set; }

        public string ReasonText { get; set; }

        public string ReasonType { get; set; }
    }
}

