namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelRoomGetRequest : ITopRequest<HotelRoomGetResponse>
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
            return "taobao.hotel.room.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("gid", this.Gid);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("need_hotel", this.NeedHotel);
            dictionary.Add("need_room_desc", this.NeedRoomDesc);
            dictionary.Add("need_room_quotas", this.NeedRoomQuotas);
            dictionary.Add("need_room_type", this.NeedRoomType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public long? Gid { get; set; }

        public long? ItemId { get; set; }

        public bool? NeedHotel { get; set; }

        public bool? NeedRoomDesc { get; set; }

        public bool? NeedRoomQuotas { get; set; }

        public bool? NeedRoomType { get; set; }
    }
}

