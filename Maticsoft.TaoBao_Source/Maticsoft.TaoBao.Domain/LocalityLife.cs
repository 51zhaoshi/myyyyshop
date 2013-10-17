namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LocalityLife : TopObject
    {
        [XmlElement("choose_logis")]
        public string ChooseLogis { get; set; }

        [XmlElement("expirydate")]
        public string Expirydate { get; set; }

        [XmlElement("merchant")]
        public string Merchant { get; set; }

        [XmlElement("network_id")]
        public string NetworkId { get; set; }

        [XmlElement("refund_ratio")]
        public long RefundRatio { get; set; }

        [XmlElement("verification")]
        public string Verification { get; set; }
    }
}

