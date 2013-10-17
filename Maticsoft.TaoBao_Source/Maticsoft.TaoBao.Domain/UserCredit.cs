namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class UserCredit : TopObject
    {
        [XmlElement("good_num")]
        public long GoodNum { get; set; }

        [XmlElement("level")]
        public long Level { get; set; }

        [XmlElement("score")]
        public long Score { get; set; }

        [XmlElement("total_num")]
        public long TotalNum { get; set; }
    }
}

