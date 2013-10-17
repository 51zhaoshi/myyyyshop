namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LotteryWangcaiPresentStat : TopObject
    {
        [XmlElement("date_id")]
        public long DateId { get; set; }

        [XmlElement("present_fee")]
        public long PresentFee { get; set; }

        [XmlElement("present_stake")]
        public long PresentStake { get; set; }

        [XmlElement("present_user")]
        public long PresentUser { get; set; }

        [XmlElement("seller_id")]
        public long SellerId { get; set; }
    }
}

