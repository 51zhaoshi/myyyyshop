namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelImageUploadRequest : ITopUploadRequest<HotelImageUploadResponse>, ITopRequest<HotelImageUploadResponse>
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
            return "taobao.hotel.image.upload";
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
            dictionary.Add("hid", this.Hid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("hid", this.Hid);
            RequestValidator.ValidateRequired("pic", this.Pic);
            RequestValidator.ValidateMaxLength("pic", this.Pic, 0x7d000);
        }

        public long? Hid { get; set; }

        public FileItem Pic { get; set; }
    }
}

