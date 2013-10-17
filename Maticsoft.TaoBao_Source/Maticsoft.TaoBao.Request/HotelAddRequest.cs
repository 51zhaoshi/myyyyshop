namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelAddRequest : ITopUploadRequest<HotelAddResponse>, ITopRequest<HotelAddResponse>
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
            return "taobao.hotel.add";
        }

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> dictionary = new Dictionary<string, FileItem>();
            dictionary.Add("pic", this.Pic);
            return dictionary;
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("address", this.Address);
            dictionary.Add("city", this.City);
            dictionary.Add("country", this.Country);
            dictionary.Add("decorate_time", this.DecorateTime);
            dictionary.Add("desc", this.Desc);
            dictionary.Add("district", this.District);
            dictionary.Add("domestic", this.Domestic);
            dictionary.Add("level", this.Level);
            dictionary.Add("name", this.Name);
            dictionary.Add("opening_time", this.OpeningTime);
            dictionary.Add("orientation", this.Orientation);
            dictionary.Add("province", this.Province);
            dictionary.Add("rooms", this.Rooms);
            dictionary.Add("service", this.Service);
            dictionary.Add("site_param", this.SiteParam);
            dictionary.Add("storeys", this.Storeys);
            dictionary.Add("tel", this.Tel);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("address", this.Address);
            RequestValidator.ValidateMaxLength("address", this.Address, 120);
            RequestValidator.ValidateRequired("city", this.City);
            RequestValidator.ValidateMaxValue("city", this.City, 0xf423fL);
            RequestValidator.ValidateMinValue("city", this.City, 0L);
            RequestValidator.ValidateRequired("country", this.Country);
            RequestValidator.ValidateMaxLength("decorate_time", this.DecorateTime, 4);
            RequestValidator.ValidateRequired("desc", this.Desc);
            RequestValidator.ValidateMaxLength("desc", this.Desc, 0xc350);
            RequestValidator.ValidateMaxValue("district", this.District, 0xf423fL);
            RequestValidator.ValidateMinValue("district", this.District, 0L);
            RequestValidator.ValidateRequired("domestic", this.Domestic);
            RequestValidator.ValidateRequired("level", this.Level);
            RequestValidator.ValidateMaxLength("level", this.Level, 1);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 60);
            RequestValidator.ValidateMaxLength("opening_time", this.OpeningTime, 4);
            RequestValidator.ValidateRequired("orientation", this.Orientation);
            RequestValidator.ValidateMaxLength("orientation", this.Orientation, 1);
            RequestValidator.ValidateRequired("pic", this.Pic);
            RequestValidator.ValidateMaxLength("pic", this.Pic, 0x7d000);
            RequestValidator.ValidateRequired("province", this.Province);
            RequestValidator.ValidateMaxValue("province", this.Province, 0xf423fL);
            RequestValidator.ValidateMinValue("province", this.Province, 0L);
            RequestValidator.ValidateMaxValue("rooms", this.Rooms, 0x270fL);
            RequestValidator.ValidateMinValue("rooms", this.Rooms, 0L);
            RequestValidator.ValidateMaxLength("site_param", this.SiteParam, 100);
            RequestValidator.ValidateMaxValue("storeys", this.Storeys, 0x270fL);
            RequestValidator.ValidateMinValue("storeys", this.Storeys, 0L);
            RequestValidator.ValidateMaxLength("tel", this.Tel, 0x20);
        }

        public string Address { get; set; }

        public long? City { get; set; }

        public string Country { get; set; }

        public string DecorateTime { get; set; }

        public string Desc { get; set; }

        public long? District { get; set; }

        public bool? Domestic { get; set; }

        public string Level { get; set; }

        public string Name { get; set; }

        public string OpeningTime { get; set; }

        public string Orientation { get; set; }

        public FileItem Pic { get; set; }

        public long? Province { get; set; }

        public long? Rooms { get; set; }

        public string Service { get; set; }

        public string SiteParam { get; set; }

        public long? Storeys { get; set; }

        public string Tel { get; set; }
    }
}

