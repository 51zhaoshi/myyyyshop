namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelRoomsSearchRequest : ITopRequest<HotelRoomsSearchResponse>
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
            return "taobao.hotel.rooms.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("gids", this.Gids);
            dictionary.Add("hids", this.Hids);
            dictionary.Add("item_ids", this.ItemIds);
            dictionary.Add("need_hotel", this.NeedHotel);
            dictionary.Add("need_room_desc", this.NeedRoomDesc);
            dictionary.Add("need_room_quotas", this.NeedRoomQuotas);
            dictionary.Add("need_room_type", this.NeedRoomType);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("rids", this.Rids);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public string Gids { get; set; }

        public string Hids { get; set; }

        public string ItemIds { get; set; }

        public bool? NeedHotel { get; set; }

        public bool? NeedRoomDesc { get; set; }

        public bool? NeedRoomQuotas { get; set; }

        public bool? NeedRoomType { get; set; }

        public long? PageNo { get; set; }

        public string Rids { get; set; }
    }
}

