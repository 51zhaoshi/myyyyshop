namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelOrderBookingFeedbackRequest : ITopRequest<HotelOrderBookingFeedbackResponse>
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
            return "taobao.hotel.order.booking.feedback";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("failed_reason", this.FailedReason);
            dictionary.Add("message_id", this.MessageId);
            dictionary.Add("oid", this.Oid);
            dictionary.Add("out_oid", this.OutOid);
            dictionary.Add("refund_code", this.RefundCode);
            dictionary.Add("result", this.Result);
            dictionary.Add("session_id", this.SessionId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("message_id", this.MessageId);
            RequestValidator.ValidateMinValue("oid", this.Oid, 0L);
            RequestValidator.ValidateRequired("out_oid", this.OutOid);
            RequestValidator.ValidateRequired("result", this.Result);
            RequestValidator.ValidateRequired("session_id", this.SessionId);
            RequestValidator.ValidateMinValue("session_id", this.SessionId, 0L);
        }

        public string FailedReason { get; set; }

        public string MessageId { get; set; }

        public long? Oid { get; set; }

        public string OutOid { get; set; }

        public string RefundCode { get; set; }

        public string Result { get; set; }

        public long? SessionId { get; set; }
    }
}

