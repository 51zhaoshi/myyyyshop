namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelNameGetRequest : ITopRequest<HotelNameGetResponse>
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
            return "taobao.hotel.name.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("city", this.City);
            dictionary.Add("country", this.Country);
            dictionary.Add("district", this.District);
            dictionary.Add("domestic", this.Domestic);
            dictionary.Add("name", this.Name);
            dictionary.Add("province", this.Province);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("domestic", this.Domestic);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 60);
        }

        public long? City { get; set; }

        public string Country { get; set; }

        public long? District { get; set; }

        public bool? Domestic { get; set; }

        public string Name { get; set; }

        public long? Province { get; set; }
    }
}

