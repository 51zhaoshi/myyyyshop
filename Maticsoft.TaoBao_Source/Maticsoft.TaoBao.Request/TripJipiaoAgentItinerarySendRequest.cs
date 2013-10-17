namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TripJipiaoAgentItinerarySendRequest : ITopRequest<TripJipiaoAgentItinerarySendResponse>
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
            return "taobao.trip.jipiao.agent.itinerary.send";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("company_code", this.CompanyCode);
            dictionary.Add("express_code", this.ExpressCode);
            dictionary.Add("itinerary_id", this.ItineraryId);
            dictionary.Add("itinerary_no", this.ItineraryNo);
            dictionary.Add("send_date", this.SendDate);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("company_code", this.CompanyCode);
            RequestValidator.ValidateMaxLength("company_code", this.CompanyCode, 20);
            RequestValidator.ValidateRequired("express_code", this.ExpressCode);
            RequestValidator.ValidateMaxLength("express_code", this.ExpressCode, 0x20);
            RequestValidator.ValidateRequired("itinerary_id", this.ItineraryId);
            RequestValidator.ValidateRequired("itinerary_no", this.ItineraryNo);
            RequestValidator.ValidateMaxLength("itinerary_no", this.ItineraryNo, 0x20);
            RequestValidator.ValidateRequired("send_date", this.SendDate);
        }

        public string CompanyCode { get; set; }

        public string ExpressCode { get; set; }

        public long? ItineraryId { get; set; }

        public string ItineraryNo { get; set; }

        public string SendDate { get; set; }
    }
}

