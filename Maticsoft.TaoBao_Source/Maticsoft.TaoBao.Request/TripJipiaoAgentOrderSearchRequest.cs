namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TripJipiaoAgentOrderSearchRequest : ITopRequest<TripJipiaoAgentOrderSearchResponse>
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
            return "taobao.trip.jipiao.agent.order.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("begin_time", this.BeginTime);
            dictionary.Add("end_time", this.EndTime);
            dictionary.Add("has_itinerary", this.HasItinerary);
            dictionary.Add("page", this.Page);
            dictionary.Add("status", this.Status);
            dictionary.Add("trip_type", this.TripType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool? HasItinerary { get; set; }

        public long? Page { get; set; }

        public long? Status { get; set; }

        public long? TripType { get; set; }
    }
}

