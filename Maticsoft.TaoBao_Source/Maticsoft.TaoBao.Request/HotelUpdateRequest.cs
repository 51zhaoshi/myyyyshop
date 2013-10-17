namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelUpdateRequest : ITopUploadRequest<HotelUpdateResponse>, ITopRequest<HotelUpdateResponse>
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
            return "taobao.hotel.update";
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
            dictionary.Add("hid", this.Hid);
            dictionary.Add("level", this.Level);
            dictionary.Add("name", this.Name);
            dictionary.Add("opening_time", this.OpeningTime);
            dictionary.Add("orientation", this.Orientation);
            dictionary.Add("province", this.Province);
            dictionary.Add("rooms", this.Rooms);
            dictionary.Add("service", this.Service);
            dictionary.Add("storeys", this.Storeys);
            dictionary.Add("tel", this.Tel);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("address", this.Address, 120);
            RequestValidator.ValidateMaxValue("city", this.City, 0xf423fL);
            RequestValidator.ValidateMinValue("city", this.City, 0L);
            RequestValidator.ValidateMaxLength("decorate_time", this.DecorateTime, 4);
            RequestValidator.ValidateMaxLength("desc", this.Desc, 0xc350);
            RequestValidator.ValidateMaxValue("district", this.District, 0xf423fL);
            RequestValidator.ValidateMinValue("district", this.District, 0L);
            RequestValidator.ValidateRequired("hid", this.Hid);
            RequestValidator.ValidateMaxLength("level", this.Level, 1);
            RequestValidator.ValidateMaxLength("name", this.Name, 60);
            RequestValidator.ValidateMaxLength("opening_time", this.OpeningTime, 4);
            RequestValidator.ValidateMaxLength("orientation", this.Orientation, 1);
            RequestValidator.ValidateMaxLength("pic", this.Pic, 0x7d000);
            RequestValidator.ValidateMaxValue("province", this.Province, 0xf423fL);
            RequestValidator.ValidateMinValue("province", this.Province, 0L);
            RequestValidator.ValidateMaxValue("rooms", this.Rooms, 0x270fL);
            RequestValidator.ValidateMinValue("rooms", this.Rooms, 0L);
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

        public long? Hid { get; set; }

        public string Level { get; set; }

        public string Name { get; set; }

        public string OpeningTime { get; set; }

        public string Orientation { get; set; }

        public FileItem Pic { get; set; }

        public long? Province { get; set; }

        public long? Rooms { get; set; }

        public string Service { get; set; }

        public long? Storeys { get; set; }

        public string Tel { get; set; }
    }
}

