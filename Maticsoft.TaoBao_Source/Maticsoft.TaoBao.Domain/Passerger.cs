namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Passerger : TopObject
    {
        [XmlElement("birthday")]
        public string Birthday { get; set; }

        [XmlElement("cert_no")]
        public string CertNo { get; set; }

        [XmlElement("cert_type")]
        public long CertType { get; set; }

        [XmlElement("ei")]
        public string Ei { get; set; }

        [XmlElement("extra")]
        public string Extra { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("passenger_type")]
        public long PassengerType { get; set; }

        [XmlElement("pnr")]
        public string Pnr { get; set; }

        [XmlElement("ticket_no")]
        public string TicketNo { get; set; }

        [XmlElement("trip_card_no")]
        public string TripCardNo { get; set; }

        [XmlElement("tuigaiqian")]
        public string Tuigaiqian { get; set; }
    }
}

