namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelOrdersSearchRequest : ITopRequest<HotelOrdersSearchResponse>
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
            return "taobao.hotel.orders.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("checkin_date_end", this.CheckinDateEnd);
            dictionary.Add("checkin_date_start", this.CheckinDateStart);
            dictionary.Add("checkout_date_end", this.CheckoutDateEnd);
            dictionary.Add("checkout_date_start", this.CheckoutDateStart);
            dictionary.Add("created_end", this.CreatedEnd);
            dictionary.Add("created_start", this.CreatedStart);
            dictionary.Add("gids", this.Gids);
            dictionary.Add("hids", this.Hids);
            dictionary.Add("need_guest", this.NeedGuest);
            dictionary.Add("need_message", this.NeedMessage);
            dictionary.Add("oids", this.Oids);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("rids", this.Rids);
            dictionary.Add("status", this.Status);
            dictionary.Add("tids", this.Tids);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("status", this.Status, 1);
        }

        public DateTime? CheckinDateEnd { get; set; }

        public DateTime? CheckinDateStart { get; set; }

        public DateTime? CheckoutDateEnd { get; set; }

        public DateTime? CheckoutDateStart { get; set; }

        public DateTime? CreatedEnd { get; set; }

        public DateTime? CreatedStart { get; set; }

        public string Gids { get; set; }

        public string Hids { get; set; }

        public bool? NeedGuest { get; set; }

        public bool? NeedMessage { get; set; }

        public string Oids { get; set; }

        public long? PageNo { get; set; }

        public string Rids { get; set; }

        public string Status { get; set; }

        public string Tids { get; set; }
    }
}

