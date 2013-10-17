namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RouteCarriageInfo : TopObject
    {
        [XmlElement("add_fee")]
        public string AddFee { get; set; }

        [XmlElement("comments")]
        public string Comments { get; set; }

        [XmlElement("give_time")]
        public string GiveTime { get; set; }

        [XmlElement("initial_fee")]
        public string InitialFee { get; set; }

        [XmlElement("least_expense")]
        public string LeastExpense { get; set; }

        [XmlElement("orig_volume_rate")]
        public string OrigVolumeRate { get; set; }

        [XmlElement("orig_weight_rate")]
        public string OrigWeightRate { get; set; }

        [XmlElement("price_description")]
        public string PriceDescription { get; set; }

        [XmlElement("take_time")]
        public string TakeTime { get; set; }

        [XmlElement("transport_mode")]
        public string TransportMode { get; set; }

        [XmlElement("transport_name")]
        public string TransportName { get; set; }

        [XmlElement("transport_time")]
        public string TransportTime { get; set; }

        [XmlElement("transport_time_hours")]
        public long TransportTimeHours { get; set; }

        [XmlElement("transport_type_code")]
        public string TransportTypeCode { get; set; }

        [XmlElement("transport_way")]
        public string TransportWay { get; set; }

        [XmlElement("volume_rate")]
        public string VolumeRate { get; set; }

        [XmlElement("weight_rate")]
        public string WeightRate { get; set; }
    }
}

