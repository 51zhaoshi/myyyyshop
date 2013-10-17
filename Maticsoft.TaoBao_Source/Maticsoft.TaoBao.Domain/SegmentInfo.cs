namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SegmentInfo : TopObject
    {
        [XmlElement("airline_code")]
        public string AirlineCode { get; set; }

        [XmlElement("arr_airport_code")]
        public string ArrAirportCode { get; set; }

        [XmlElement("arr_city_code")]
        public string ArrCityCode { get; set; }

        [XmlElement("arr_time")]
        public string ArrTime { get; set; }

        [XmlElement("book_status")]
        public long BookStatus { get; set; }

        [XmlElement("cabin_class")]
        public long CabinClass { get; set; }

        [XmlElement("cabin_code")]
        public string CabinCode { get; set; }

        [XmlElement("cabin_id")]
        public long CabinId { get; set; }

        [XmlElement("carrier")]
        public string Carrier { get; set; }

        [XmlElement("child_fee")]
        public long ChildFee { get; set; }

        [XmlElement("child_price")]
        public long ChildPrice { get; set; }

        [XmlElement("child_tax")]
        public long ChildTax { get; set; }

        [XmlElement("dep_airport_code")]
        public string DepAirportCode { get; set; }

        [XmlElement("dep_city_code")]
        public string DepCityCode { get; set; }

        [XmlElement("dep_time")]
        public string DepTime { get; set; }

        [XmlElement("extra")]
        public string Extra { get; set; }

        [XmlElement("fee")]
        public long Fee { get; set; }

        [XmlElement("flight_id")]
        public long FlightId { get; set; }

        [XmlElement("flight_no")]
        public string FlightNo { get; set; }

        [XmlElement("flight_type")]
        public string FlightType { get; set; }

        [XmlElement("memo")]
        public string Memo { get; set; }

        [XmlArray("passengers"), XmlArrayItem("passerger")]
        public List<Passerger> Passengers { get; set; }

        [XmlElement("policy_id")]
        public long PolicyId { get; set; }

        [XmlElement("policy_type")]
        public long PolicyType { get; set; }

        [XmlElement("price")]
        public long Price { get; set; }

        [XmlElement("segment_type")]
        public long SegmentType { get; set; }

        [XmlElement("special_rule")]
        public string SpecialRule { get; set; }

        [XmlElement("tax")]
        public long Tax { get; set; }

        [XmlElement("ticket_price")]
        public long TicketPrice { get; set; }
    }
}

