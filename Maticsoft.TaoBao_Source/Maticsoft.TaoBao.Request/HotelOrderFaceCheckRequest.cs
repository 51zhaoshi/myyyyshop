namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelOrderFaceCheckRequest : ITopRequest<HotelOrderFaceCheckResponse>
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
            return "taobao.hotel.order.face.check";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("checked", this.Checked);
            dictionary.Add("checkin_date", this.CheckinDate);
            dictionary.Add("checkout_date", this.CheckoutDate);
            dictionary.Add("oid", this.Oid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("checked", this.Checked);
            RequestValidator.ValidateRequired("oid", this.Oid);
        }

        public bool? Checked { get; set; }

        public DateTime? CheckinDate { get; set; }

        public DateTime? CheckoutDate { get; set; }

        public long? Oid { get; set; }
    }
}

