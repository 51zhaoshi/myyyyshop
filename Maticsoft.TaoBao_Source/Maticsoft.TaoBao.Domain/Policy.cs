namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Policy : TopObject
    {
        [XmlElement("agent_id")]
        public long AgentId { get; set; }

        [XmlElement("airline")]
        public string Airline { get; set; }

        [XmlElement("arr_airports")]
        public string ArrAirports { get; set; }

        [XmlElement("attributes")]
        public string Attributes { get; set; }

        [XmlElement("auto_hk_flag")]
        public bool AutoHkFlag { get; set; }

        [XmlElement("auto_ticket_flag")]
        public bool AutoTicketFlag { get; set; }

        [XmlElement("cabin_rules")]
        public string CabinRules { get; set; }

        [XmlElement("dep_airports")]
        public string DepAirports { get; set; }

        [XmlElement("first_sale_advance_day")]
        public long FirstSaleAdvanceDay { get; set; }

        [XmlElement("flight_info")]
        public string FlightInfo { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("last_sale_advance_day")]
        public long LastSaleAdvanceDay { get; set; }

        [XmlElement("out_product_id")]
        public string OutProductId { get; set; }

        [XmlElement("policy_detail")]
        public Maticsoft.TaoBao.Domain.PolicyDetail PolicyDetail { get; set; }

        [XmlElement("policy_type")]
        public long PolicyType { get; set; }

        [XmlElement("sale_end_date")]
        public string SaleEndDate { get; set; }

        [XmlElement("sale_start_date")]
        public string SaleStartDate { get; set; }

        [XmlElement("seat_info")]
        public string SeatInfo { get; set; }

        [XmlElement("share_support")]
        public bool ShareSupport { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }

        [XmlElement("travel_end_date")]
        public string TravelEndDate { get; set; }

        [XmlElement("travel_start_date")]
        public string TravelStartDate { get; set; }

        [XmlElement("trip_type")]
        public long TripType { get; set; }
    }
}

