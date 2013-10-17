namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelRoomUpdateRequest : ITopUploadRequest<HotelRoomUpdateResponse>, ITopRequest<HotelRoomUpdateResponse>
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
            return "taobao.hotel.room.update";
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
            dictionary.Add("gid", this.Gid);
            dictionary.Add("guide", this.Guide);
            dictionary.Add("multi_room_quotas", this.MultiRoomQuotas);
            dictionary.Add("payment_type", this.PaymentType);
            dictionary.Add("pic_path", this.PicPath);
            dictionary.Add("price_type", this.PriceType);
            dictionary.Add("room_quotas", this.RoomQuotas);
            dictionary.Add("service", this.Service);
            dictionary.Add("site_param", this.SiteParam);
            dictionary.Add("size", this.Size);
            dictionary.Add("status", this.Status);
            dictionary.Add("storey", this.Storey);
            dictionary.Add("title", this.Title);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("area", this.Area, 1);
            RequestValidator.ValidateMaxLength("bbn", this.Bbn, 1);
            RequestValidator.ValidateMaxLength("bed_type", this.BedType, 1);
            RequestValidator.ValidateMaxLength("breakfast", this.Breakfast, 1);
            RequestValidator.ValidateMaxValue("deposit", this.Deposit, 0x5f5e09cL);
            RequestValidator.ValidateMinValue("deposit", this.Deposit, 0L);
            RequestValidator.ValidateMaxLength("desc", this.Desc, 0xc350);
            RequestValidator.ValidateMaxValue("fee", this.Fee, 0x5f5e09cL);
            RequestValidator.ValidateMinValue("fee", this.Fee, 0L);
            RequestValidator.ValidateRequired("gid", this.Gid);
            RequestValidator.ValidateMaxLength("guide", this.Guide, 0x1f40);
            RequestValidator.ValidateMaxLength("payment_type", this.PaymentType, 1);
            RequestValidator.ValidateMaxLength("pic", this.Pic, 0x7d000);
            RequestValidator.ValidateMaxLength("price_type", this.PriceType, 1);
            RequestValidator.ValidateMaxLength("size", this.Size, 1);
            RequestValidator.ValidateMaxLength("storey", this.Storey, 8);
            RequestValidator.ValidateMaxLength("title", this.Title, 90);
        }

        public string Area { get; set; }

        public string Bbn { get; set; }

        public string BedType { get; set; }

        public string Breakfast { get; set; }

        public long? Deposit { get; set; }

        public string Desc { get; set; }

        public long? Fee { get; set; }

        public long? Gid { get; set; }

        public string Guide { get; set; }

        public string MultiRoomQuotas { get; set; }

        public string PaymentType { get; set; }

        public FileItem Pic { get; set; }

        public string PicPath { get; set; }

        public string PriceType { get; set; }

        public string RoomQuotas { get; set; }

        public string Service { get; set; }

        public string SiteParam { get; set; }

        public string Size { get; set; }

        public long? Status { get; set; }

        public string Storey { get; set; }

        public string Title { get; set; }
    }
}

