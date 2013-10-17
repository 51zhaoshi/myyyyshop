namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TradeShippingaddressUpdateRequest : ITopRequest<TradeShippingaddressUpdateResponse>
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
            return "taobao.trade.shippingaddress.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("receiver_address", this.ReceiverAddress);
            dictionary.Add("receiver_city", this.ReceiverCity);
            dictionary.Add("receiver_district", this.ReceiverDistrict);
            dictionary.Add("receiver_mobile", this.ReceiverMobile);
            dictionary.Add("receiver_name", this.ReceiverName);
            dictionary.Add("receiver_phone", this.ReceiverPhone);
            dictionary.Add("receiver_state", this.ReceiverState);
            dictionary.Add("receiver_zip", this.ReceiverZip);
            dictionary.Add("tid", this.Tid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("receiver_address", this.ReceiverAddress, 0xe4);
            RequestValidator.ValidateMaxLength("receiver_city", this.ReceiverCity, 0x20);
            RequestValidator.ValidateMaxLength("receiver_district", this.ReceiverDistrict, 0x20);
            RequestValidator.ValidateMaxLength("receiver_mobile", this.ReceiverMobile, 30);
            RequestValidator.ValidateMaxLength("receiver_name", this.ReceiverName, 50);
            RequestValidator.ValidateMaxLength("receiver_phone", this.ReceiverPhone, 30);
            RequestValidator.ValidateMaxLength("receiver_state", this.ReceiverState, 0x20);
            RequestValidator.ValidateMaxLength("receiver_zip", this.ReceiverZip, 6);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        public string ReceiverAddress { get; set; }

        public string ReceiverCity { get; set; }

        public string ReceiverDistrict { get; set; }

        public string ReceiverMobile { get; set; }

        public string ReceiverName { get; set; }

        public string ReceiverPhone { get; set; }

        public string ReceiverState { get; set; }

        public string ReceiverZip { get; set; }

        public long? Tid { get; set; }
    }
}

