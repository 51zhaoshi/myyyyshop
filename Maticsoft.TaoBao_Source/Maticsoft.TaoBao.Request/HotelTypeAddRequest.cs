namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelTypeAddRequest : ITopRequest<HotelTypeAddResponse>
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
            return "taobao.hotel.type.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("hid", this.Hid);
            dictionary.Add("name", this.Name);
            dictionary.Add("site_param", this.SiteParam);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("hid", this.Hid);
            RequestValidator.ValidateMinValue("hid", this.Hid, 0L);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 30);
            RequestValidator.ValidateMaxLength("site_param", this.SiteParam, 100);
        }

        public long? Hid { get; set; }

        public string Name { get; set; }

        public string SiteParam { get; set; }
    }
}

