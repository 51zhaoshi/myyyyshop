namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelRoomAddRequest : ITopUploadRequest<HotelRoomAddResponse>, ITopRequest<HotelRoomAddResponse>
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
            return "taobao.hotel.room.add";
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
            dictionary.Add("area", this.Area);
            dictionary.Add("bbn", this.Bbn);
            dictionary.Add("bed_type", this.BedType);
            dictionary.Add("breakfast", this.Breakfast);
            dictionary.Add("deposit", this.Deposit);
            dictionary.Add("desc", this.Desc);
            dictionary.Add("fee", this.Fee);
            dictionary.Add("guide", this.Guide);
            dictionary.Add("hid", this.Hid);
            dictionary.Add("multi_room_quotas", this.MultiRoomQuotas);
            dictionary.Add("payment_type", this.PaymentType);
            dictionary.Add("pic_path", this.PicPath);
            dictionary.Add("price_type", this.PriceType);
            dictionary.Add("rid", this.Rid);
            dictionary.Add("room_quotas", this.RoomQuotas);
            dictionary.Add("service", this.Service);
            dictionary.Add("site_param", this.SiteParam);
            dictionary.Add("size", this.Size);
            dictionary.Add("storey", this.Storey);
            dictionary.Add("title", this.Title);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("area", this.Area, 1);
            RequestValidator.ValidateMaxLength("bbn", this.Bbn, 1);
            RequestValidator.ValidateRequired("bed_type", this.BedType);
            RequestValidator.ValidateMaxLength("bed_type", this.BedType, 1);
            RequestValidator.ValidateRequired("breakfast", this.Breakfast);
            RequestValidator.ValidateMaxLength("breakfast", this.Breakfast, 1);
            RequestValidator.ValidateMaxValue("deposit", this.Deposit, 0x5f5e09cL);
            RequestValidator.ValidateMinValue("deposit", this.Deposit, 0L);
            RequestValidator.ValidateRequired("desc", this.Desc);
            RequestValidator.ValidateMaxLength("desc", this.Desc, 0xc350);
            RequestValidator.ValidateMaxValue("fee", this.Fee, 0x5f5e09cL);
            RequestValidator.ValidateMinValue("fee", this.Fee, 0L);
            RequestValidator.ValidateRequired("guide", this.Guide);
            RequestValidator.ValidateMaxLength("guide", this.Guide, 0x1f40);
            RequestValidator.ValidateRequired("hid", this.Hid);
            RequestValidator.ValidateRequired("payment_type", this.PaymentType);
            RequestValidator.ValidateMaxLength("payment_type", this.PaymentType, 1);
            RequestValidator.ValidateMaxLength("pic", this.Pic, 0x7d000);
            RequestValidator.ValidateMaxLength("price_type", this.PriceType, 1);
            RequestValidator.ValidateRequired("rid", this.Rid);
            RequestValidator.ValidateMaxLength("site_param", this.SiteParam, 100);
            RequestValidator.ValidateMaxLength("size", this.Size, 1);
            RequestValidator.ValidateMaxLength("storey", this.Storey, 8);
            RequestValidator.ValidateRequired("title", this.Title);
            RequestValidator.ValidateMaxLength("title", this.Title, 90);
        }

        public string Area { get; set; }

        public string Bbn { get; set; }

        public string BedType { get; set; }

        public string Breakfast { get; set; }

        public long? Deposit { get; set; }

        public string Desc { get; set; }

        public long? Fee { get; set; }

        public string Guide { get; set; }

        public long? Hid { get; set; }

        public string MultiRoomQuotas { get; set; }

        public string PaymentType { get; set; }

        public FileItem Pic { get; set; }

        public string PicPath { get; set; }

        public string PriceType { get; set; }

        public long? Rid { get; set; }

        public string RoomQuotas { get; set; }

        public string Service { get; set; }

        public string SiteParam { get; set; }

        public string Size { get; set; }

        public string Storey { get; set; }

        public string Title { get; set; }
    }
}

