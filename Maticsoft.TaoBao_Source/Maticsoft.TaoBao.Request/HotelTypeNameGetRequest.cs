namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelTypeNameGetRequest : ITopRequest<HotelTypeNameGetResponse>
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
            return "taobao.hotel.type.name.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("hid", this.Hid);
            dictionary.Add("name", this.Name);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("hid", this.Hid);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 60);
        }

        public long? Hid { get; set; }

        public string Name { get; set; }
    }
}

