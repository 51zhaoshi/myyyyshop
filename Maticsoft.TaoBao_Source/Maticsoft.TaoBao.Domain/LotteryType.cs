namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LotteryType : TopObject
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}

