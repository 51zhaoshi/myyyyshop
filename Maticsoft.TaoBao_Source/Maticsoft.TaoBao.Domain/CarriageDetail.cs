namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CarriageDetail : TopObject
    {
        [XmlElement("add_fee")]
        public long AddFee { get; set; }

        [XmlElement("add_weight")]
        public long AddWeight { get; set; }

        [XmlElement("damage_payment")]
        public string DamagePayment { get; set; }

        [XmlElement("got_time")]
        public string GotTime { get; set; }

        [XmlElement("initial_fee")]
        public long InitialFee { get; set; }

        [XmlElement("initial_weight")]
        public long InitialWeight { get; set; }

        [XmlElement("lost_payment")]
        public string LostPayment { get; set; }

        [XmlElement("way_day")]
        public string WayDay { get; set; }
    }
}

