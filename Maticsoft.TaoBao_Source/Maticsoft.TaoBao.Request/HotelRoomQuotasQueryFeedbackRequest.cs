namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelRoomQuotasQueryFeedbackRequest : ITopRequest<HotelRoomQuotasQueryFeedbackResponse>
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
            return "taobao.hotel.room.quotas.query.feedback";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("avaliable_room_count", this.AvaliableRoomCount);
            dictionary.Add("checkin_date", this.CheckinDate);
            dictionary.Add("checkout_date", this.CheckoutDate);
            dictionary.Add("failed_reason", this.FailedReason);
            dictionary.Add("message_id", this.MessageId);
            dictionary.Add("result", this.Result);
            dictionary.Add("room_quotas", this.RoomQuotas);
            dictionary.Add("total_room_price", this.TotalRoomPrice);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("avaliable_room_count", this.AvaliableRoomCount);
            RequestValidator.ValidateMinValue("avaliable_room_count", this.AvaliableRoomCount, 0L);
            RequestValidator.ValidateRequired("checkin_date", this.CheckinDate);
            RequestValidator.ValidateRequired("checkout_date", this.CheckoutDate);
            RequestValidator.ValidateRequired("message_id", this.MessageId);
            RequestValidator.ValidateRequired("result", this.Result);
            RequestValidator.ValidateRequired("total_room_price", this.TotalRoomPrice);
            RequestValidator.ValidateMaxValue("total_room_price", this.TotalRoomPrice, 0x5f5e0ffL);
            RequestValidator.ValidateMinValue("total_room_price", this.TotalRoomPrice, 0L);
        }

        public long? AvaliableRoomCount { get; set; }

        public DateTime? CheckinDate { get; set; }

        public DateTime? CheckoutDate { get; set; }

        public string FailedReason { get; set; }

        public string MessageId { get; set; }

        public string Result { get; set; }

        public string RoomQuotas { get; set; }

        public long? TotalRoomPrice { get; set; }
    }
}

