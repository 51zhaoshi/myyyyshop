namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JipiaoPolicyProcessRequest : ITopRequest<JipiaoPolicyProcessResponse>
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
            return "taobao.jipiao.policy.process";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("airline", this.Airline);
            dictionary.Add("arr_airports", this.ArrAirports);
            dictionary.Add("attributes", this.Attributes);
            dictionary.Add("auto_hk_flag", this.AutoHkFlag);
            dictionary.Add("auto_ticket_flag", this.AutoTicketFlag);
            dictionary.Add("cabin_rules", this.CabinRules);
            dictionary.Add("change_rule", this.ChangeRule);
            dictionary.Add("day_of_weeks", this.DayOfWeeks);
            dictionary.Add("dep_airports", this.DepAirports);
            dictionary.Add("ei", this.Ei);
            dictionary.Add("exclude_date", this.ExcludeDate);
            dictionary.Add("first_sale_advance_day", this.FirstSaleAdvanceDay);
            dictionary.Add("flags", this.Flags);
            dictionary.Add("flight_info", this.FlightInfo);
            dictionary.Add("last_sale_advance_day", this.LastSaleAdvanceDay);
            dictionary.Add("memo", this.Memo);
            dictionary.Add("office_id", this.OfficeId);
            dictionary.Add("out_product_id", this.OutProductId);
            dictionary.Add("policy_id", this.PolicyId);
            dictionary.Add("policy_type", this.PolicyType);
            dictionary.Add("refund_rule", this.RefundRule);
            dictionary.Add("reissue_rule", this.ReissueRule);
            dictionary.Add("sale_end_date", this.SaleEndDate);
            dictionary.Add("sale_start_date", this.SaleStartDate);
            dictionary.Add("seat_info", this.SeatInfo);
            dictionary.Add("share_support", this.ShareSupport);
            dictionary.Add("special_rule", this.SpecialRule);
            dictionary.Add("travel_end_date", this.TravelEndDate);
            dictionary.Add("travel_start_date", this.TravelStartDate);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("airline", this.Airline);
            RequestValidator.ValidateMaxLength("airline", this.Airline, 2);
            RequestValidator.ValidateRequired("arr_airports", this.ArrAirports);
            RequestValidator.ValidateMaxLength("arr_airports", this.ArrAirports, 100);
            RequestValidator.ValidateMaxLength("attributes", this.Attributes, 300);
            RequestValidator.ValidateRequired("cabin_rules", this.CabinRules);
            RequestValidator.ValidateMaxLength("cabin_rules", this.CabinRules, 300);
            RequestValidator.ValidateMaxLength("change_rule", this.ChangeRule, 300);
            RequestValidator.ValidateRequired("day_of_weeks", this.DayOfWeeks);
            RequestValidator.ValidateMaxLength("day_of_weeks", this.DayOfWeeks, 7);
            RequestValidator.ValidateRequired("dep_airports", this.DepAirports);
            RequestValidator.ValidateMaxLength("dep_airports", this.DepAirports, 100);
            RequestValidator.ValidateMaxLength("ei", this.Ei, 20);
            RequestValidator.ValidateMaxLength("exclude_date", this.ExcludeDate, 200);
            RequestValidator.ValidateMaxValue("flags", this.Flags, 0x7fffffffffffffffL);
            RequestValidator.ValidateMinValue("flags", this.Flags, 0L);
            RequestValidator.ValidateMaxLength("flight_info", this.FlightInfo, 0x3e8);
            RequestValidator.ValidateMaxLength("memo", this.Memo, 500);
            RequestValidator.ValidateMaxLength("office_id", this.OfficeId, 0x20);
            RequestValidator.ValidateMaxLength("out_product_id", this.OutProductId, 0x40);
            RequestValidator.ValidateRequired("policy_type", this.PolicyType);
            RequestValidator.ValidateMaxLength("refund_rule", this.RefundRule, 300);
            RequestValidator.ValidateMaxLength("reissue_rule", this.ReissueRule, 300);
            RequestValidator.ValidateRequired("sale_end_date", this.SaleEndDate);
            RequestValidator.ValidateRequired("sale_start_date", this.SaleStartDate);
            RequestValidator.ValidateMaxLength("seat_info", this.SeatInfo, 0x5dc);
            RequestValidator.ValidateMaxLength("special_rule", this.SpecialRule, 300);
            RequestValidator.ValidateRequired("travel_end_date", this.TravelEndDate);
            RequestValidator.ValidateRequired("travel_start_date", this.TravelStartDate);
            RequestValidator.ValidateRequired("type", this.Type);
            RequestValidator.ValidateMaxValue("type", this.Type, 2L);
            RequestValidator.ValidateMinValue("type", this.Type, 0L);
        }

        public string Airline { get; set; }

        public string ArrAirports { get; set; }

        public string Attributes { get; set; }

        public bool? AutoHkFlag { get; set; }

        public bool? AutoTicketFlag { get; set; }

        public string CabinRules { get; set; }

        public string ChangeRule { get; set; }

        public string DayOfWeeks { get; set; }

        public string DepAirports { get; set; }

        public string Ei { get; set; }

        public string ExcludeDate { get; set; }

        public long? FirstSaleAdvanceDay { get; set; }

        public long? Flags { get; set; }

        public string FlightInfo { get; set; }

        public long? LastSaleAdvanceDay { get; set; }

        public string Memo { get; set; }

        public string OfficeId { get; set; }

        public string OutProductId { get; set; }

        public string PolicyId { get; set; }

        public long? PolicyType { get; set; }

        public string RefundRule { get; set; }

        public string ReissueRule { get; set; }

        public DateTime? SaleEndDate { get; set; }

        public DateTime? SaleStartDate { get; set; }

        public string SeatInfo { get; set; }

        public bool? ShareSupport { get; set; }

        public string SpecialRule { get; set; }

        public DateTime? TravelEndDate { get; set; }

        public DateTime? TravelStartDate { get; set; }

        public long? Type { get; set; }
    }
}

