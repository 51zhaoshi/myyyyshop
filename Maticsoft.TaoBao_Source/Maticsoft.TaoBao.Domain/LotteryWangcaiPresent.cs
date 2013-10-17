namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LotteryWangcaiPresent : TopObject
    {
        [XmlElement("lottery_name")]
        public string LotteryName { get; set; }

        [XmlElement("present_date")]
        public string PresentDate { get; set; }

        [XmlElement("stake_count")]
        public long StakeCount { get; set; }

        [XmlElement("user_nick")]
        public string UserNick { get; set; }

        [XmlElement("win_fee")]
        public long WinFee { get; set; }
    }
}

